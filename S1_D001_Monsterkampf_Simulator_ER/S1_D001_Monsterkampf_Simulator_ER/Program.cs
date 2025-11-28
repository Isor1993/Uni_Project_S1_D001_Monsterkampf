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
using S1_D001_Monsterkampf_Simulator_ER.Controllers;
using S1_D001_Monsterkampf_Simulator_ER.Dependencies;
using S1_D001_Monsterkampf_Simulator_ER.Factories;
using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Player;
using S1_D001_Monsterkampf_Simulator_ER.Systems.Damage;

namespace S1_D001_Monsterkampf_Simulator_ER
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // === CORE SYSTEMS ===
            var diagnostics = new DiagnosticsManager();
            var random = new RandomManager();
            var symbol = new SymbolManager();
            var balancing = new MonsterBalancing(diagnostics);
            var pipeline = new DamagePipeline(diagnostics);
            var print = new PrintManager(diagnostics);

            // === INPUT SYSTEM (Keyboard) ===

            var keyboardInput = new KeyboardInputManager();      // implements IPlayerInput
            var inputManager = new InputManager(keyboardInput);  // Stat choice menu controller
            IPlayerInput playerInput = new KeyboardInputManager();

            // === UI SYSTEM ===
            var ui = new UIManager(symbol, diagnostics, balancing);

            // === FACTORIES ===
            var monsterFactory = new MonsterFactory(diagnostics, balancing);

            // === CONTROLLER ===
            var playerController = new PlayerController(null!, diagnostics, ui, keyboardInput);
            var enemyController = new EnemyController(null!, diagnostics, random);
            var screen = new ScreenManager(symbol, playerController);

            // === GAME DEPENDENCIES ===
            var gameDeps = new GameDependencies(pipeline, ui, playerInput, inputManager, random, diagnostics, print, playerController, balancing, enemyController, monsterFactory, screen);

            // === BATTLEMANAGER DEPENDENCIES (initial leer) ===
            var emptyBattleDeps = new BattleManagerDependencies(
                PlayerController: playerController,
                EnemyController: enemyController,
                Diagnostics: diagnostics,
                Random: random,
                Pipeline: pipeline,
                PlayerData: new PlayerData(),
                Balancing: balancing,
                UI: ui
            );

            // === GAMEMANAGER erstellen ===
            var game = new GameManager(gameDeps, inputManager, playerInput);

            // === SPIEL STARTEN ===
            game.RunGame();
        }
    }
}