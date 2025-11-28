/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : BattleManager.cs
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
*   Handles the complete turn-based combat system between player and enemy.
*   Manages round flow, UI updates, skill execution, damage calculation,
*   passive triggers, DOT effects, cooldown ticking and battle outcome.
*
* Responsibilities :
*   - Determine who starts based on Speed
*   - Coordinate player and enemy turns
*   - Apply start-/end-of-turn status effects
*   - Execute skills and handle resulting effects
*   - Detect death, calculate results and trigger rewards
*   - Refresh combat UI (skill box, info boxes, sprites)
*
* History :
*   xx.xx.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Controllers;
using S1_D001_Monsterkampf_Simulator_ER.Dependencies;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Skills;
using S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects;

namespace S1_D001_Monsterkampf_Simulator_ER.Managers
{
    /// <summary>
    /// Represents the possible outcomes of a battle.
    /// </summary>
    internal enum BattleResult
    {
        None,
        PlayerWon,
        EnemyWon
    }

    internal class BattleManager
    {

        // === Dependencies ===
        private readonly BattleManagerDependencies _deps;
        private readonly InputManager _inputManager;
        private readonly IPlayerInput _playerInput;
        // === Fields ===
        private bool _playerStarts;

        /// <summary>
        /// Gets the player controller assigned by dependencies.
        /// Provides access to the player's monster and skill decisions.
        /// </summary>
        private ControllerBase PlayerController => _deps.PlayerController;

        /// <summary>
        /// Gets the enemy controller used to automate enemy skill decisions.
        /// </summary>
        private ControllerBase EnemyController => _deps.EnemyController;

        /// <summary>
        /// Shortcut accessor for the player's monster instance.
        /// </summary>
        private MonsterBase Player => PlayerController.Monster;

        /// <summary>
        /// Shortcut accessor for the enemy's monster instance.
        /// </summary>
        private MonsterBase Enemy => EnemyController.Monster;

        /// <summary>
        /// Total rounds played in the current battle.
        /// Used for post-battle statistics in UI.
        /// </summary>
        public int TotalRounds { get; set; }

        /// <summary>
        /// Creates a new BattleManager responsible for executing a full combat encounter.
        /// </summary>
        /// <param name="deps">Grouped dependencies for battle logic.</param>
        /// <param name="inputManager">Manages user confirmation inputs.</param>
        /// <param name="playerInput">Reads player commands during turns.</param>
        /// <exception cref="ArgumentNullException">Thrown if dependencies are missing.</exception>
        public BattleManager(BattleManagerDependencies deps, InputManager inputManager, IPlayerInput playerInput)
        {
            _deps = deps ?? throw new ArgumentNullException(nameof(deps));
            _inputManager = inputManager;
            _playerInput = playerInput;
        }

        /// <summary>
        /// Runs the complete battle loop until one monster dies.
        /// Handles round flow, alternating turns, UI updates and effect processing.
        /// </summary>
        public void RunBattle()
        {

            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(RunBattle)}: Battle started!");

            int round = 1;
            bool battleRunning = true;
            // Determine who starts
            if (Player.Meta.Speed >= Enemy.Meta.Speed)
            {
                _playerStarts = true;
                _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(RunBattle)}: Player begins the battle (Speed {Player.Meta.Speed} >= {Enemy.Meta.Speed}).");
            }
            else
            {
                _playerStarts = false;
                _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(RunBattle)}: Enemy begins the battle (Speed {Enemy.Meta.Speed} > {Player.Meta.Speed}).");
            }

            while (battleRunning)
            {
                TotalRounds++;
                _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(RunBattle)}: === ROUND {round} ===");

                // Refresh combat UI
                _deps.UI.ClearSkillBox();
                _deps.UI.PrintMonsterInfoBoxPlayer();
                _deps.UI.PrintMonsterInfoBoxEnemy();

                Player.PrintSprite(true);
                Enemy.PrintSprite(false);

                RefreshUI();

                // Apply start-of-turn effects
                HandleStartOfTurnEffects();


                // Turns
                if (_playerStarts)
                {
                    if (PlayerTurn()) break;
                    if (EnemyTurn()) break;
                }
                else
                {
                    if (EnemyTurn()) break;
                    if (PlayerTurn()) break;
                }

                // Apply end-of-turn DOT, statuses & cooldowns
                HandleEndOfTurnEffects();

                // Death check after effects
                if (CheckDeathsAfterEndOfTurn())
                {
                    break;
                }

                round++;
            }

            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(HandleEndOfTurnEffects)}: Battle ended!");

            // Result and rewards
            BattleResult result = DetermineBattleResult();

            ControllerBase? winner = result switch
            {
                BattleResult.PlayerWon => PlayerController,
                BattleResult.EnemyWon => EnemyController,
                _ => null
            };

            if (winner != null)
            {
                HandleVictory(winner);
            }

            ShowBattleOutcome(result);
        }

        /// <summary>
        /// Applies all start-of-turn effects such as regeneration,
        /// buff triggers and passive activation for both monsters.
        /// </summary>
        private void HandleStartOfTurnEffects()
        {
            Player.ProcessStartOfTurnEffects();
            Player.UsePasiveSkill();
            Enemy.ProcessStartOfTurnEffects();
            Enemy.UsePasiveSkill();
        }

        /// <summary>
        /// Applies all end-of-turn effect logic including DOT damage,
        /// status effect ticking and cooldown ticking.
        /// </summary>
        private void HandleEndOfTurnEffects()
        {
            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(HandleEndOfTurnEffects)}: End-of-turn Effects begin.");

            Player.ProcessEndOfTurnEffects();
            Enemy.ProcessEndOfTurnEffects();

            Player.ProcessStatusEffectDurations();
            Enemy.ProcessStatusEffectDurations();

            TickCooldowns();

            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(HandleEndOfTurnEffects)}: End-of-turn Effects complete.");
        }

        /// <summary>
        /// Checks whether either monster died after DOT or other end-of-turn effects.
        /// </summary>
        /// <returns>True if the battle should end.</returns>
        private bool CheckDeathsAfterEndOfTurn()
        {
            if (!Enemy.IsAlive)
            {
                _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}: Enemy died from DOT effects! Battle over.");
                return true;
            }

            if (!Player.IsAlive)
            {
                _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}{nameof(CheckDeathsAfterEndOfTurn)} : Player died from DOT effects! Battle over.");
                return true;
            }

            return false;
        }

        /// <summary>
        /// Reduces cooldown values for all skills on both monsters.
        /// </summary>
        private void TickCooldowns()
        {
            Player.ProcessSkillCooldowns();
            Enemy.ProcessSkillCooldowns();
        }

        /// <summary>
        /// Determines the winner based on monster life state.
        /// </summary>
        /// <returns>The resulting BattleResult enum value.</returns>
        public BattleResult DetermineBattleResult()
        {
            if (Player.IsAlive && !Enemy.IsAlive)
                return BattleResult.PlayerWon;

            if (!Player.IsAlive && Enemy.IsAlive)
                return BattleResult.EnemyWon;

            return BattleResult.None;
        }

        /// <summary>
        /// Prints a diagnostic message summarizing the final result of the battle.
        /// </summary>
        /// <param name="result">The result enum describing who won.</param>
        private void ShowBattleOutcome(BattleResult result)
        {
            switch (result)
            {
                case BattleResult.PlayerWon:
                    _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}{nameof(ShowBattleOutcome)} : Player WON the battle!");
                    break;

                case BattleResult.EnemyWon:
                    _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}{nameof(ShowBattleOutcome)} : Enemy WON the battle!");
                    break;

                default:
                    _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}{nameof(ShowBattleOutcome)} : Battle ended with UNKNOWN result.");
                    break;
            }
        }

        /// <summary>
        /// Applies reward logic for the winning monster.  
        /// Only the player receives rewards (stat points), including passive modifiers.
        /// </summary>
        /// <param name="winner">The controller whose monster won the fight.</param>
        private void HandleVictory(ControllerBase winner)
        {
            MonsterBase winnerMonster = winner.Monster;
            bool winnerIsPlayer = winner == PlayerController;

            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(HandleVictory)}: Victory triggered for {winnerMonster.Race}. WinnerIsPlayer={winnerIsPlayer}");

            if (!winnerIsPlayer)
                return;


            float reward = _deps.Balancing.BaseVictoryReward;

            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(HandleVictory)}: BaseReward={reward} (Balancing)");

            // Passive skill reward modification
            if (winnerMonster.SkillPackage.PassiveSkill != null)
            {
                reward = winnerMonster.SkillPackage.PassiveSkill.ModifyVictoryReward(reward);

                _deps.Diagnostics.AddCheck(
                    $"{nameof(BattleManager)}.{nameof(HandleVictory)}: Passive victory modifier applied → Reward now {reward}");
            }
            else
            {
                _deps.Diagnostics.AddCheck(
                    $"{nameof(BattleManager)}.{nameof(HandleVictory)}: No passive victory modifier found.");
            }

            int finalReward = (int)MathF.Round(reward);

            _deps.PlayerData.UnassignedStatPoints += finalReward;
            _deps.PlayerData.CompletedBattles++;

            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(HandleVictory)}: Player receives {finalReward} stat point(s). Total={_deps.PlayerData.UnassignedStatPoints}, Battles={_deps.PlayerData.CompletedBattles}");
        }

        /// <summary>
        /// Pauses the battle flow and waits for player confirmation (ENTER).
        /// </summary>
        private void WaitForNext()
        {
            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(WaitForNext)}: Waiting for user input...");
            _inputManager.WaitForEnter(_playerInput);
        }

        /// <summary>
        /// Refreshes monster info boxes for both combatants.
        /// Does not touch the SkillBox (managed by PlayerController).
        /// </summary>
        private void RefreshUI()
        {
            _deps.UI.UpdateMonsterBoxPlayer(Player);
            _deps.UI.UpdateMonsterBoxEnemy(Enemy);

            // SkillBox NICHT anfassen – PlayerController macht das selbst
            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(RefreshUI)}: Monster UI refreshed.");
        }

        /// <summary>
        /// Handles the player's entire turn including:
        /// skill selection, damage execution, effect detection,
        /// UI updates and death check.
        /// </summary>
        /// <returns>True if the enemy died and battle should end.</returns>
        private bool PlayerTurn()
        {
            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(PlayerTurn)}: Player turn started.");

            // 1. Skill selection
            SkillBase skill = PlayerController.ChooseSkill();

            _deps.UI.ClearSkillBox();

            int effectCountBefore = Enemy.GetStatusEffects<StatusEffectBase>().Count();
            // 2. Execute attack
            float damage = Player.Attack(Enemy, skill, _deps.Pipeline);
            var effectsAfter = Enemy.GetStatusEffects<StatusEffectBase>().ToList();
            StatusEffectBase? newEffect = null;
            if (effectsAfter.Count > effectCountBefore)
            {
                newEffect = effectsAfter.Last(); // 🎯 den neuesten Effekt nehmen
                _deps.Diagnostics.AddCheck(
                    $"{nameof(BattleManager)}.{nameof(PlayerTurn)}: New effect triggered → {newEffect.Name}");
            }

            // 3. Attack info box
            RefreshUI();
            _deps.UI.UpdateMessageBoxForAttack(Player, Enemy, skill, damage, newEffect);

            // 4. Wait for player confirmation
            WaitForNext();

            // 5. Death check

            if (!Enemy.IsAlive)
            {
                _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(PlayerTurn)}: Enemy died from player attack.");
                return true;
            }

            return false;
        }

        /// <summary>
        /// Handles the enemy’s complete turn including:
        /// AI skill selection, attack execution, applying effects,
        /// UI update and player death detection.
        /// </summary>
        /// <returns>True if the player died and the battle should end.</returns>
        private bool EnemyTurn()
        {
            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(EnemyTurn)}: Enemy turn started.");

            // 1. Enemy selects a skill
            SkillBase skill = EnemyController.ChooseSkill();

            // 2. Execute attack
            int effectCountBefore = Player.GetStatusEffects<StatusEffectBase>().Count();
            float damage = Enemy.Attack(Player, skill, _deps.Pipeline);
            var effectsAfter = Player.GetStatusEffects<StatusEffectBase>().ToList();
            StatusEffectBase? newEffect = null;

            if (effectsAfter.Count > effectCountBefore)
            {
                newEffect = effectsAfter.Last(); // 🎯 neuesten Effekt holen
                _deps.Diagnostics.AddCheck(
                    $"{nameof(BattleManager)}.{nameof(EnemyTurn)}: New effect triggered → {newEffect.Name}");
            }


            // 3. Damage info box
            RefreshUI();
            _deps.UI.UpdateMessageBoxForTakeDamage(Enemy, Player, skill: skill, damage: damage, newEffect);
            // 4. Wait for ENTER
            WaitForNext();

            // 5. Death check
            if (!Player.IsAlive)
            {
                _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(EnemyTurn)}: Player died from enemy attack.");
                return true;
            }

            return false;
        }
    }
}