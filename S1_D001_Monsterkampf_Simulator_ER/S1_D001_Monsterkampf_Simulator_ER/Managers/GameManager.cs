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

        }


        private void StartBattle()
        {

        }


        private void HandleRewards()
        {

        }


        private void NextStage()
        {

        }


        private void Endscreen()
        {

        }


        private void QuitGame()
        {

        }
    }
}
