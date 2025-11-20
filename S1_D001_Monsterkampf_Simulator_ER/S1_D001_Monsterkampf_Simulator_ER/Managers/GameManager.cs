/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : 
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
* *
* History :
* xx.xx.2025 ER Created
******************************************************************************/


using S1_D001_Monsterkampf_Simulator_ER.Dependencies;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Player;

namespace S1_D001_Monsterkampf_Simulator_ER.Managers
{

    internal class GameManager
    {
        private enum GameState { Start, Tutorial, ChooseMonster, BattleStart, HandleRewards, NextStage, End, Quit }

        // === Dependencies ===
        private readonly GameDependencies _deps;

        // === Fields ===
        private GameState _currentState = GameState.Start;
        private bool isRunning = true;
        public PlayerData PlayerData { get; } = new PlayerData();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameDependencies"></param>
        public GameManager(GameDependencies gameDependencies)
        {
            _deps = gameDependencies;

        }



        public void RunGame()
        {
            while (isRunning)
            {

                switch (_currentState)
                {
                    case GameState.Start:
                        _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(RunGame)}: Entered GameState.Start successfully.");
                        StartScreen();
                        break;

                    case GameState.Tutorial:
                        _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(RunGame)}: Entered GameState.Tutorial successfully.");
                        TutorialScreen();
                        break;

                    case GameState.ChooseMonster:
                        _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(RunGame)}: Entered GameState.ChooseMonster successfully.");
                        ChooseMonster();
                        break;

                    case GameState.BattleStart:
                        _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(RunGame)}: Entered GameState.BattleStart successfully.");
                        StartBattle();
                        break;

                    case GameState.HandleRewards:
                        _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(RunGame)}: Entered GameState.HandleRewards successfully.");
                        HandleRewards();
                        break;

                    case GameState.NextStage:
                        _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(RunGame)}: Entered GameState.NextStage successfully.");
                        NextStage();
                        break;

                    case GameState.End:
                        _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(RunGame)}: Entered GameState.End successfully.");
                        Endscreen();
                        break;

                    case GameState.Quit:
                        _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(RunGame)}: Entered GameState.Quit successfully.");
                        QuitGame();
                        break;

                    default:
                        _deps.Diagnostics.AddError($"{nameof(GameManager)}.{nameof(RunGame)}: No possible state found.");
                        break;
                }
            }
        }


        private void StartScreen()
        {

        }


        private void TutorialScreen()
        {

        }


        private void GameLoop()
        {

        }


        private void ChooseMonster()
        {
            //TODO wegmachen nach UI Test: _deps.UI.ShowMonsterSelectionMenu();
            //TODO wegmachen nach UI Test: RaceType choosenRace = _deps.Input.ReadMonsterChoice();
        }


        private void StartBattle()
        {

        }


        private void HandleRewards()
        {
            _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(HandleRewards)}: Handling rewards...");

            MonsterBase player = _deps.PlayerController.Monster;

            while (PlayerData.UnassignedStatPoints > 0)
            {
                _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(HandleRewards)}: Player has {PlayerData.UnassignedStatPoints} stat point(s) to assign.");
                //TODO siehe UIManager
                _deps.UI.ShowStatDistributionMenu(PlayerData.UnassignedStatPoints);

                StatType choice = _deps.Input.ReadStatIncreaseChoice();

                player.ApplyStatPointIncrease(choice, _deps.Balancing);

                PlayerData.UnassignedStatPoints--;

                _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(HandleRewards)}: Assigned 1 point to {choice}. Remaining={PlayerData.UnassignedStatPoints}.");
            }

            int newLevel = player.Level + _deps.Balancing.LevelUpScaling;

            MonsterMeta newMeta = _deps.Balancing.GetMeta(player.Race, newLevel);
            player.ApplyLevelUp(newMeta);

            _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(HandleRewards)}: Player leveled up to {player.Level}. Stats updated!");

            _currentState = GameState.NextStage;
        }


        private void NextStage()
        {
            _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(NextStage)}: Preparing next stage...");

            MonsterBase player= _deps.PlayerController.Monster;

            player.Meta.CurrentHP = player.Meta.MaxHP;

            int completed = PlayerData.CompletedBattles;
            int bonusLevels= completed/_deps.Balancing.BonusLevels;

            int enemyLevel = player.Level + bonusLevels;
            _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(NextStage)}: EnemyLevel = {enemyLevel} (player {player.Level}, bonus {bonusLevels}).");

            RaceType enemyRace= _deps.Random.PickRandomRace(player.Race);
            _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(NextStage)}: Next enemy is {enemyRace}.");

            MonsterBase enemy = _deps.MonsterFactory.Create(enemyRace, enemyLevel);

            _deps.EnemyController.SetMonster(enemy);
            _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(NextStage)}: Enemy created & assigned.");
            _currentState = GameState.BattleStart;
        }


        private void Endscreen()
        {

        }


        private void QuitGame()
        {

        }
    }
}
