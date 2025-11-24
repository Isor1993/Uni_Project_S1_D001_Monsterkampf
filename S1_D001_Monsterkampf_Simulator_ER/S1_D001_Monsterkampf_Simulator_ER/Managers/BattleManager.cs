/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : BattleManager.cs
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
* Handles the complete turn-based combat flow between two monsters.
*
* History :
* xx.xx.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Controllers;
using S1_D001_Monsterkampf_Simulator_ER.Dependencies;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Skills;
using S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects;


namespace S1_D001_Monsterkampf_Simulator_ER.Managers
{
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
        private ControllerBase PlayerController => _deps.PlayerController;
        private ControllerBase EnemyController => _deps.EnemyController;

        private MonsterBase Player => PlayerController.Monster;
        private MonsterBase Enemy => EnemyController.Monster;
        public int TotalRounds { get;  set; }
        public BattleManager(BattleManagerDependencies deps, InputManager inputManager, IPlayerInput playerInput)
        {
            _deps = deps ?? throw new ArgumentNullException(nameof(deps));
            _inputManager = inputManager;
            _playerInput = playerInput;
        }

        public void RunBattle()
        {

            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(RunBattle)}: Battle started!");

            int round = 1;
            bool battleRunning = true;
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

                // UI vorbereiten
                _deps.UI.ClearSkillBox();
                _deps.UI.PrintMonsterInfoBoxPlayer();
                _deps.UI.PrintMonsterInfoBoxEnemy();
                // TODO Sprites hier
                _deps.UI.PrintSlimeP();
                _deps.UI.PrintSlimeE();
                RefreshUI();

                // Start-of-turn Effekte
                HandleStartOfTurnEffects();

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

                // End-of-turn Effekte
                HandleEndOfTurnEffects();

                // DOT Death check
                if (CheckDeathsAfterEndOfTurn())
                {
                    break;
                }

                round++;
            }
            
            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(HandleEndOfTurnEffects)}: Battle ended!");

            BattleResult result = DetermineBattleResult();

            ControllerBase? winner = result switch
            {
                BattleResult.PlayerWon => PlayerController,
                BattleResult.EnemyWon => EnemyController,
                _ => null
            };

            // VictoryHook triggern
            if (winner != null)
            {
                HandleVictory(winner);
            }

            ShowBattleOutcome(result);
        }


        private void HandleStartOfTurnEffects()
        {
            Player.ProcessStartOfTurnEffects();
            Enemy.ProcessStartOfTurnEffects();
        }

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

        private void TickCooldowns()
        {
            Player.ProcessSkillCooldowns();
            Enemy.ProcessSkillCooldowns();
        }
        public BattleResult DetermineBattleResult()
        {
            if (Player.IsAlive && !Enemy.IsAlive)
                return BattleResult.PlayerWon;

            if (!Player.IsAlive && Enemy.IsAlive)
                return BattleResult.EnemyWon;

            return BattleResult.None;
        }
        //TODO vernetzen später an richtige stelle
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
        private void HandleVictory(ControllerBase winner)
        {
            MonsterBase winnerMonster = winner.Monster;
            bool winnerIsPlayer = winner == PlayerController;

            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(HandleVictory)}: Victory triggered for {winnerMonster.Race}. WinnerIsPlayer={winnerIsPlayer}");

            if (!winnerIsPlayer)
                return;

            // === Base Reward ===
            float reward = _deps.Balancing.BaseVictoryReward;

            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(HandleVictory)}: BaseReward={reward} (Balancing)");

            // === Passive Victory-Modifikatoren (Greed, future skills) ===
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

            int finalReward = (int)MathF.Round(reward); // oder MathF.Round(reward)
            // === PlayerData aktualisieren ===
            _deps.PlayerData.UnassignedStatPoints += finalReward;
            _deps.PlayerData.CompletedBattles++;

            _deps.Diagnostics.AddCheck(
                $"{nameof(BattleManager)}.{nameof(HandleVictory)}: Player receives {finalReward} stat point(s). Total={_deps.PlayerData.UnassignedStatPoints}, Battles={_deps.PlayerData.CompletedBattles}");
        }

        private void WaitForNext()
        {
            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(WaitForNext)}: Waiting for user input...");
            _inputManager.WaitForEnter(_playerInput);
        }

        private void RefreshUI()
        {
            _deps.UI.UpdateMonsterBoxPlayer(Player);
            _deps.UI.UpdateMonsterBoxEnemy(Enemy);

            // SkillBox NICHT anfassen – PlayerController macht das selbst
            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(RefreshUI)}: Monster UI refreshed.");
        }

        private bool PlayerTurn()
        {
            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(PlayerTurn)}: Player turn started.");

            // 1. Skill auswählen
            SkillBase skill = PlayerController.ChooseSkill();

            _deps.UI.ClearSkillBox();

            int effectCountBefore = Enemy.GetStatusEffects<StatusEffectBase>().Count();
            // 2. Schaden berechnen
            float damage = Player.Attack(Enemy, skill, _deps.Pipeline);
            var effectsAfter = Enemy.GetStatusEffects<StatusEffectBase>().ToList();
            StatusEffectBase? newEffect = null;
            if (effectsAfter.Count > effectCountBefore)
            {
                newEffect = effectsAfter.Last(); // 🎯 den neuesten Effekt nehmen
                _deps.Diagnostics.AddCheck(
                    $"{nameof(BattleManager)}.{nameof(PlayerTurn)}: New effect triggered → {newEffect.Name}");
            }

            // 3. Attack-MessageBox anzeigen
            RefreshUI();
            _deps.UI.UpdateMessageBoxForAttack(Player, Enemy, skill, damage, newEffect);

            // 4. Warten auf Bestätigung
            WaitForNext();

            // 5. UI aktualisieren

            // 6. Enemy tot?
            if (!Enemy.IsAlive)
            {
                _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(PlayerTurn)}: Enemy died from player attack.");
                return true;
            }

            return false;
        }
        private bool EnemyTurn()
        {
            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(EnemyTurn)}: Enemy turn started.");

            // 1. Gegner wählt Skill
            SkillBase skill = EnemyController.ChooseSkill();

            // 2. Schaden berechnen
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


            // 3. TakeDamage MessageBox anzeigen
            RefreshUI();
            _deps.UI.UpdateMessageBoxForTakeDamage(Enemy, Player, skill: skill, damage: damage, newEffect);
            // 4. Spieler muss ENTER drücken
            WaitForNext();

            // 5. UI aktualisieren

            // 6. Player tot?
            if (!Player.IsAlive)
            {
                _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(EnemyTurn)}: Player died from enemy attack.");
                return true;
            }

            return false;
        }
    }
}