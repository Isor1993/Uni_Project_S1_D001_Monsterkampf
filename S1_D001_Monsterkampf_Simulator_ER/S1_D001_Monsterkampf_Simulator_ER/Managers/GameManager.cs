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

using S1_D001_Monsterkampf_Simulator_ER.Balancing;
using S1_D001_Monsterkampf_Simulator_ER.Dependencies;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Player;

namespace S1_D001_Monsterkampf_Simulator_ER.Managers
{
    internal class GameManager
    {
        private enum GameState
        { Start, Tutorial, ChooseMonster, BattleStart, HandleRewards, NextStage, End, Quit }

        // === Dependencies ===
        private readonly GameDependencies _deps;

        private readonly InputManager _inputManager;
        private readonly IPlayerInput _playerInput;

        // === Fields ===
        private GameState _currentState = GameState.Start;

        private bool isRunning = true;
        public PlayerData PlayerData { get; } = new PlayerData();

        public static int totalFights { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="gameDependencies"></param>
        public GameManager(GameDependencies gameDependencies, InputManager inputManager, IPlayerInput playerInput)
        {
            _deps = gameDependencies;
            _inputManager = inputManager;
            _playerInput = playerInput;
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
            Console.Clear();
            _deps.Screen.PrintScreenStart();
            Console.ReadKey(true);
            _currentState = GameState.Tutorial;
        }

        private void TutorialScreen()
        {
            Console.Clear();
            _deps.Screen.PrintScreenTutorial();
            Console.ReadKey(true);

            _currentState = GameState.ChooseMonster;
        }

        private void ChooseMonster()
        {
            Console.Clear();
            _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(ChooseMonster)}: Monster selection started.");

            // 1. Monster-Liste (4 Stück)
            RaceType[] monsterChoices = new[] { RaceType.Slime, RaceType.Goblin, RaceType.Orc, RaceType.Troll };

            // 2. UI vorbereiten

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
            // 3. Erste Anzeige
            _deps.UI.UpdateMonsterSelectionBox(monsterChoices, pointer);
            RefreshMessageBox();

            // 4. Pointer bewegen
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

            // 5. Auswahl übernehmen
            RaceType chosenRace = monsterChoices[pointer];
            int startLevel = _deps.Balancing.StartLevel;

            MonsterBase playerMonster = _deps.MonsterFactory.Create(chosenRace, startLevel);
            _deps.PlayerController.SetMonster(playerMonster);

            _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(ChooseMonster)}: Player selected {chosenRace} at Level {startLevel}.");

            // 6. Weiter
            _currentState = GameState.NextStage;
        }

        private void StartBattle()
        {
            _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(StartBattle)}: Battle starting...");

            // BattleManagerDependencies frisch erzeugen
            var battleDeps = new BattleManagerDependencies(_deps.PlayerController, _deps.EnemyController, _deps.Diagnostics, _deps.Random, _deps.DamagePipeline, PlayerData, _deps.Balancing, _deps.UI);

            // BattleManager erzeugen
            BattleManager battle = new BattleManager(battleDeps, _inputManager, _playerInput);

            // Kampf ausführen
            battle.RunBattle();

            // Ergebnis bestimmen
            BattleResult result = battle.DetermineBattleResult();
            totalFights++;
            if (result == BattleResult.PlayerWon)
            {
                _deps.UI.UpdateMessageBoxRoundInfo(true, totalFights, battle.TotalRounds);
                _deps.InputManager.WaitForEnter(_playerInput);
                _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(StartBattle)}: Player won the battle.");
                _currentState = GameState.HandleRewards;
            }
            else
            {
                _deps.UI.UpdateMessageBoxRoundInfo(false, totalFights, battle.TotalRounds);
                _deps.InputManager.WaitForEnter(_playerInput);
                _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(StartBattle)}: Player lost the battle.");
                _currentState = GameState.End;
            }
            battle.TotalRounds = 0;
        }

        private void HandleRewards()
        {
            _deps.Diagnostics.AddCheck($"{nameof(GameManager)}.{nameof(HandleRewards)}: Handling rewards...");

            MonsterBase player = _deps.PlayerController.Monster;

            // --- 1. Alle StatPoints verteilen ---
            while (PlayerData.UnassignedStatPoints > 0)
            {
                _deps.Diagnostics.AddCheck(
                    $"{nameof(GameManager)}.{nameof(HandleRewards)}: Player has {PlayerData.UnassignedStatPoints} stat point(s).");

                // UI anzeigen
                _deps.UI.PrintSkillBoxLayout();  // nutzt dieselbe Box für Stat-Auswahl
                _deps.UI.PrintMessageBoxLayout();

                // Pointer-Menü aktivieren (neue Methode in InputManager)
                StatType choice = _deps.InputManager.ReadStatIncreaseChoice(_deps.UI, player);

                // Stat erhöhen
                player.ApplyStatPointIncrease(choice, _deps.Balancing);
                PlayerData.UnassignedStatPoints--;

                _deps.Diagnostics.AddCheck(
                    $"{nameof(GameManager)}.{nameof(HandleRewards)}: +1 {choice}. Remaining={PlayerData.UnassignedStatPoints}");
            }

            // --- 2. LevelUp berechnen ---
            int newLevel = player.Level + _deps.Balancing.LevelUpScaling;

            player.ApplyLevelUp(player.Meta, _deps.Balancing);
            player.SkillPackage.ResetCooldowns();
            _deps.Diagnostics.AddCheck(
                $"{nameof(GameManager)}.{nameof(HandleRewards)}: Player leveled up to {player.Level}. Stats updated!");

            // --- 3. Weiter zu NextStage ---
            _currentState = GameState.NextStage;
        }

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

        public void ScaleEnemy(MonsterBase enemy, int level, MonsterBalancing balancing)
        {
            enemy.Meta.MaxHP += (1 + balancing.HPScaling) * level;
            enemy.Meta.AP += (1 +

            balancing.APScaling) * level;
            enemy.Meta.DP += (1 + balancing.DPScaling) * level;
            enemy.Meta.Speed += (1 + balancing.SpeedScaling) * level;

            enemy.Meta.CurrentHP = enemy.Meta.MaxHP;
        }

        private void Endscreen()
        {
            Console.Clear();
            _deps.Screen.PrintScreenEnd();
            Console.ReadKey(true);

            _currentState = GameState.Quit;
        }

        private void QuitGame()
        {
            isRunning = false;
        }
    }
}