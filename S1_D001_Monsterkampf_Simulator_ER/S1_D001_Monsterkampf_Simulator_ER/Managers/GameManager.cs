/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : GameManager.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Central state machine that controls the entire game flow of the
*   Monsterkampf-Simulator. Handles transitions between start screen,
*   tutorial, monster selection, battles, rewards, level-ups, stage scaling,
*   and final end screen. Coordinates all dependencies and global systems.
*
* Responsibilities :
*   - Maintain and switch GameState (Start → Tutorial → ChooseMonster → ...)
*   - Trigger all major game screen renders (start/tutorial/end)
*   - Initialize battles and evaluate results
*   - Manage player stat rewards, level-ups and HP resets
*   - Spawn and scale new enemy monsters for the next stage
*   - Track total fights & completed battles
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Balancing;
using S1_D001_Monsterkampf_Simulator_ER.Dependencies;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Player;

namespace S1_D001_Monsterkampf_Simulator_ER.Managers
{
    internal class GameManager
    {
        /// <summary>
        /// All possible states of the main game flow.
        /// </summary>
        private enum GameState
        {
            Start,
            Tutorial,
            ChooseMonster,
            BattleStart,
            HandleRewards,
            NextStage,
            End,
            Quit
        }

        // === Dependencies ===
        private readonly GameDependencies _deps;

        private readonly InputManager _inputManager;
        private readonly IPlayerInput _playerInput;

        // === Fields ===
        private GameState _currentState = GameState.Start;

        private bool isRunning = true;

        /// <summary>
        /// Holds general player progression values
        /// such as completed battles and unassigned stat points.
        /// </summary>
        public PlayerData PlayerData { get; } = new PlayerData();

        /// <summary>
        /// Tracks how many battles were played in the entire session.
        /// </summary>
        public static int TotalFights { get; private set; }

        /// <summary>
        /// Creates the GameManager and injects all required systems.
        /// </summary>
        /// <param name="gameDependencies">Full dependency bundle (UI, controllers, random, factory, etc.)</param>
        /// <param name="inputManager">Handles pointer-style menu navigation</param>
        /// <param name="playerInput">Reads actual input commands from keyboard</param>
        public GameManager(GameDependencies gameDependencies, InputManager inputManager, IPlayerInput playerInput)
        {
            _deps = gameDependencies;
            _inputManager = inputManager;
            _playerInput = playerInput;
        }

        /// <summary>
        /// Main loop of the entire game. Runs until QuitGame() is called.
        /// Handles state transitions and ensures each screen/step is executed.
        /// </summary>
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

        /// <summary>
        /// Shows the animated start screen and waits for player input.
        /// </summary>
        private void StartScreen()
        {
            Console.Clear();
            _deps.Screen.PrintScreenStart();
            Console.ReadKey(true);
            _currentState = GameState.Tutorial;
        }

        /// <summary>
        /// Shows tutorial instructions and waits for confirmation.
        /// </summary>
        private void TutorialScreen()
        {
            Console.Clear();
            _deps.Screen.PrintScreenTutorial();
            Console.ReadKey(true);

            _currentState = GameState.ChooseMonster;
        }

        /// <summary>
        /// Lets the player select one of four starting monsters.
        /// Updates UI live as the pointer moves. Creates the chosen monster.
        /// </summary>
        private void ChooseMonster()
        {
            Console.Clear();
            _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(ChooseMonster)}: Monster selection started.");

            RaceType[] monsterChoices = new[] { RaceType.Slime, RaceType.Goblin, RaceType.Orc, RaceType.Troll };

            int pointer = 0;
            bool confirmed = false;
            _deps.UI.ShowMonsterSelectionMenu(monsterChoices, pointer);
            void RefreshMessageBox()
            {
                RaceType race = monsterChoices[pointer];
                int level = _deps.Balancing.StartLevel;

                MonsterMeta meta = _deps.Balancing.GetMeta(race, level);
                MonsterBase temp = _deps.MonsterFactory.Create(race, level);
                string desc = temp.Description;

                _deps.UI.UpdateMessageBoxForMonsterChoice(race, meta, desc);
            }
            _deps.UI.UpdateMonsterSelectionBox(monsterChoices, pointer);
            RefreshMessageBox();

            while (!confirmed)
            {
                PlayerCommand cmd = _deps.Input.ReadCommand();

                switch (cmd)
                {
                    case PlayerCommand.MoveUp:
                        pointer = Math.Max(0, pointer - 1);
                        break;

                    case PlayerCommand.MoveDown:
                        pointer = Math.Min(monsterChoices.Length - 1, pointer + 1);
                        break;

                    case PlayerCommand.Confirm:
                        confirmed = true;
                        break;
                }

                _deps.UI.UpdateMonsterSelectionBox(monsterChoices, pointer);
                RefreshMessageBox();
            }

            RaceType chosenRace = monsterChoices[pointer];
            int startLevel = _deps.Balancing.StartLevel;

            MonsterBase playerMonster = _deps.MonsterFactory.Create(chosenRace, startLevel);
            _deps.PlayerController.SetMonster(playerMonster);

            _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(ChooseMonster)}: Player selected {chosenRace} at Level {startLevel}.");

            _currentState = GameState.NextStage;
        }

        /// <summary>
        /// Starts a new battle, evaluates result, updates fight counter,
        /// and decides whether to give rewards or show the end screen.
        /// </summary>
        private void StartBattle()
        {
            _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(StartBattle)}: Battle starting...");

            var battleDeps = new BattleManagerDependencies(_deps.PlayerController, _deps.EnemyController, _deps.Diagnostics, _deps.Random, _deps.DamagePipeline, PlayerData, _deps.Balancing, _deps.UI);

            BattleManager battle = new BattleManager(battleDeps, _inputManager, _playerInput);

            battle.RunBattle();

            BattleResult result = battle.DetermineBattleResult();
            TotalFights++;
            if (result == BattleResult.PlayerWon)
            {
                _deps.UI.UpdateMessageBoxRoundInfo(true, TotalFights, battle.TotalRounds);
                _deps.InputManager.WaitForEnter(_playerInput);
                _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(StartBattle)}: Player won the battle.");
                _currentState = GameState.HandleRewards;
            }
            else
            {
                _deps.UI.UpdateMessageBoxRoundInfo(false, TotalFights, battle.TotalRounds);
                _deps.InputManager.WaitForEnter(_playerInput);
                _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(StartBattle)}: Player lost the battle.");
                _currentState = GameState.End;
            }
            battle.TotalRounds = 0;
        }

        /// <summary>
        /// Prepares the next enemy: resets player HP, calculates bonus scaling,
        /// picks random race, creates & scales enemy, and moves to BattleStart.
        /// </summary>
        private void HandleRewards()
        {
            _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(HandleRewards)}: Handling rewards...");

            MonsterBase player = _deps.PlayerController.Monster;

            while (PlayerData.UnassignedStatPoints > 0)
            {
                _deps.Diagnostics.AddCheck(
                    $"{nameof(GameManager)}.{nameof(HandleRewards)}: Player has {PlayerData.UnassignedStatPoints} stat point(s).");

                _deps.UI.PrintSkillBoxLayout();
                _deps.UI.PrintMessageBoxLayout();

                StatType choice = _deps.InputManager.ReadStatIncreaseChoice(_deps.UI, player);

                player.ApplyStatPointIncrease(choice, _deps.Balancing);
                PlayerData.UnassignedStatPoints--;

                _deps.Diagnostics.AddCheck(
                    $"{nameof(GameManager)}.{nameof(HandleRewards)}: +1 {choice}. Remaining={PlayerData.UnassignedStatPoints}");
            }

            int newLevel = player.Level + _deps.Balancing.LevelUpScaling;

            player.ApplyLevelUp(_deps.Balancing);
            player.SkillPackage.ResetCooldowns();
            _deps.Diagnostics.AddCheck(
                $"{nameof(GameManager)}.{nameof(HandleRewards)}: Player leveled up to {player.Level}. Stats updated!");

            _currentState = GameState.NextStage;
        }

        /// <summary>
        /// Scales enemy stats based on level and balancing curves.
        /// </summary>
        private void NextStage()
        {
            _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(NextStage)}: Preparing next stage...");

            MonsterBase player = _deps.PlayerController.Monster;

            player.Meta.CurrentHP = player.Meta.MaxHP;

            int completed = PlayerData.CompletedBattles;
            int bonusLevels = completed / _deps.Balancing.BonusLevels;

            int enemyLevel = player.Level + bonusLevels;
            _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(NextStage)}: EnemyLevel = {enemyLevel} (player {player.Level}, bonus {bonusLevels}).");

            RaceType enemyRace = _deps.Random.PickRandomRace(player.Race);
            _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(NextStage)}: Next enemy is {enemyRace}.");

            MonsterBase enemy = _deps.MonsterFactory.Create(enemyRace, enemyLevel);
            ScaleEnemy(enemy, enemyLevel

                , _deps.Balancing);
            _deps.EnemyController.SetMonster(enemy);
            _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(NextStage)}: Enemy created & assigned.");
            _currentState = GameState.BattleStart;
        }

        /// <summary>
        /// Renders end screen and waits for confirmation before quitting.
        /// </summary>
        public void ScaleEnemy(MonsterBase enemy, int level, MonsterBalancing balancing)
        {
            enemy.Meta.MaxHP += (1 + balancing.HPScaling) * level;
            enemy.Meta.AP += (1 +

            balancing.APScaling) * level;
            enemy.Meta.DP += (1 + balancing.DPScaling) * level;
            enemy.Meta.Speed += (1 + balancing.SpeedScaling) * level;

            enemy.Meta.CurrentHP = enemy.Meta.MaxHP;
        }

        /// <summary>
        /// Stops the main loop and ends the game session.
        /// </summary>
        private void Endscreen()
        {
            Console.Clear();
            _deps.Screen.PrintScreenEnd();
            Console.ReadKey(true);

            _currentState = GameState.Quit;
        }

        /// <summary>
        ///
        /// </summary>
        private void QuitGame()
        {
            isRunning = false;
        }
    }
}