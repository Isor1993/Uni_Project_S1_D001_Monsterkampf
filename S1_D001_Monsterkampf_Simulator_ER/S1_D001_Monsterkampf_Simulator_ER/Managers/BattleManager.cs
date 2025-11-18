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
using S1_D001_Monsterkampf_Simulator_ER.Skills.Goblin;
using S1_D001_Monsterkampf_Simulator_ER.Systems.Damage;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

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
        // === Fields ===
        private ControllerBase PlayerController => _deps.PlayerController;
        private ControllerBase EnemyController => _deps.EnemyController;

        private MonsterBase Player => PlayerController.Monster;
        private MonsterBase Enemy => EnemyController.Monster;

        public BattleManager(BattleManagerDependencies deps)
        {
            _deps = deps ?? throw new ArgumentNullException(nameof(deps));
        }

        public void RunBattle()
        {

            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(RunBattle)}: Battle started!");

            int round = 1;
            bool battleRunning = true;

            while (battleRunning)
            {

                _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(RunBattle)}: === ROUND {round} ===");

                HandleStartOfTurnEffects();
                (SkillBase playerSkill, SkillBase enemySkill) = ChooseSkills();
                var (first, second, firstSkill, secondSkill) = PrepareTurnOrder(playerSkill, enemySkill);

                if (ExecuteAttackPhase(first, second, firstSkill, secondSkill))
                {
                    break;
                }

                HandleEndOfTurnEffects();
                if (CheckDeathsAfterEndOfTurn())
                {
                    break;
                }
                _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(HandleEndOfTurnEffects)}: End-of-turn Effects begin.");
                TickCooldowns();
                _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(HandleEndOfTurnEffects)}: Cooldowns tick.");
                round++;
            }
            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(HandleEndOfTurnEffects)}: Battle ended!");
            //TODO erklären
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

        private (ControllerBase first, ControllerBase second) DetermineTurnOrder()
        {
            float playerSpeed = Player.Meta.Speed;
            float enemySpeed = Enemy.Meta.Speed;

            if (playerSpeed > enemySpeed)
            {
                _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(DetermineTurnOrder)}: Player goes first (Speed {playerSpeed} > {enemySpeed}).");
                return (PlayerController, EnemyController);
            }
            if (enemySpeed > playerSpeed)
            {
                _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(DetermineTurnOrder)}: Enemy goes first (Speed {enemySpeed} > {playerSpeed}).");
                return (EnemyController, PlayerController);
            }

            int roll = _deps.Random.Next(2);

            if (roll == 0)
            {
                _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(DetermineTurnOrder)}: Equal speed -> Player randomly chosen to go first.");
                return (PlayerController, EnemyController);
            }

            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(DetermineTurnOrder)}: Equal speed -> Enemy randomly chosen to go first.");
            return (EnemyController, PlayerController);
        }
        private void HandleStartOfTurnEffects()
        {
            Player.ProcessStartOfTurnEffects();
            Enemy.ProcessStartOfTurnEffects();
        }

        private void ExecuteAttack(ControllerBase attacker, ControllerBase defender, SkillBase skill)
        {
            float damage = attacker.Monster.Attack(defender.Monster, skill, _deps.Pipeline);
            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(ExecuteAttack)}: {attacker.Monster.Race} used {skill.Name} and dealt {damage} damage to {defender.Monster.Race}.");
        }

        private (SkillBase playerSkill, SkillBase enemySkill) ChooseSkills()
        {
            SkillBase playerSkill = PlayerController.ChooseSkill();
            SkillBase enemySkill = EnemyController.ChooseSkill();

            return (playerSkill, enemySkill);
        }

        private (ControllerBase first, ControllerBase second, SkillBase firstSkill, SkillBase secondSkill) PrepareTurnOrder(SkillBase playerSkill, SkillBase enemySkill)
        {
            (ControllerBase first, ControllerBase second) = DetermineTurnOrder();

            SkillBase firstSkill = first == PlayerController ? playerSkill : enemySkill;
            SkillBase secondSkill = second == PlayerController ? playerSkill : enemySkill;

            return (first, second, firstSkill, secondSkill);
        }

        private bool ExecuteAttackPhase(ControllerBase first, ControllerBase second, SkillBase firstSkill, SkillBase secondSkill)
        {
            // Attack 1
            ExecuteAttack(first, second, firstSkill);

            if (!second.Monster.IsAlive)
            {
                _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(ExecuteAttackPhase)}: {second.Monster.Race} died! Battle over.");
                return true;
            }

            // Attack 2
            ExecuteAttack(second, first, secondSkill);

            if (!first.Monster.IsAlive)
            {
                _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(ExecuteAttackPhase)}: {first.Monster.Race} died! Battle over.");
                return true;
            }

            

            return false;
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
        private BattleResult DetermineBattleResult()
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

            // === Base Reward bestimmen ===
            int baseReward = _deps.Balancing.BaseVictoryReward;

            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(HandleVictory)}: BaseReward={baseReward} (Balancing)");

            var greed = winnerMonster.SkillPackage.EventPassives.OfType<PassiveSkill_Greed>().FirstOrDefault();

            int finalReward = baseReward;

            if (greed != null)
            {
                finalReward = (int)greed.ApplyRewardBonus(baseReward);

                _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(HandleVictory)}: Greed active → FinalReward={finalReward}");
            }
            else
            {
                _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(HandleVictory)}: No Greed passive found.");

            }
            _deps.PlayerData.UnassignedStatPoints += finalReward;
            _deps.PlayerData.CompletedBattles++;

            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}.{nameof(HandleVictory)}: Player receives {finalReward} stat point(s).Total={_deps.PlayerData.UnassignedStatPoints}, Battles={_deps.PlayerData.CompletedBattles}");
        }
    }
}