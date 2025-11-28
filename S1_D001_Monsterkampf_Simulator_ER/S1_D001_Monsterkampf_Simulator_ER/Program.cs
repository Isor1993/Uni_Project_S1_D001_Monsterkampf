/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : Program.cs
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
*   Entry point of the Monster Battle Simulator.
*   Bootstraps all core systems, initializes dependency injection,
*   constructs global managers, and starts the game flow.
*
* Responsibilities :
*   - Create and wire all global managers and systems
*   - Initialize a single unified input system (IPlayerInput)
*   - Provide complete GameDependencies for the GameManager
*   - Start the game loop via GameManager.RunGame()
*
* History :
*   xx.xx.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Balancing;
using S1_D001_Monsterkampf_Simulator_ER.Controllers;
using S1_D001_Monsterkampf_Simulator_ER.Dependencies;
using S1_D001_Monsterkampf_Simulator_ER.Factories;
using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Systems.Damage;

namespace S1_D001_Monsterkampf_Simulator_ER
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // === CORE SYSTEMS ====================================================
            var diagnostics = new DiagnosticsManager();
            var random = new RandomManager();
            var symbol = new SymbolManager();
            var print = new PrintManager(diagnostics);
            var balancing = new MonsterBalancing(diagnostics);
            var pipeline = new DamagePipeline(diagnostics);

            // === INPUT SYSTEM =====================================================
            
            IPlayerInput input = new KeyboardInputManager();
            var inputManager = new InputManager(input);

            // === UI SYSTEM ========================================================
            var ui = new UIManager(symbol, diagnostics, balancing);

            // === FACTORIES =========================================================
            var monsterFactory = new MonsterFactory(diagnostics, balancing);

            // === CONTROLLERS =======================================================
            
            var playerController = new PlayerController(
                monster: null!, 
                diagnostics: diagnostics,
                ui: ui,
                input: input);

            var enemyController = new EnemyController(
                monster: null!,
                diagnostics: diagnostics,
                random: random);

            // === SCREEN SYSTEM =====================================================
            var screen = new ScreenManager(symbol, playerController);

            // === GAME DEPENDENCIES ================================================
            var gameDeps = new GameDependencies(
                DamagePipeline: pipeline,
                UI: ui,
                input,
                InputManager: inputManager,
                Random: random,
                Diagnostics: diagnostics,
                Print: print,
                PlayerController: playerController,
                Balancing: balancing,
                EnemyController: enemyController,
                MonsterFactory: monsterFactory,
                Screen: screen);

            // === GAME MANAGER ======================================================
            var game = new GameManager(gameDeps, inputManager, input);

            // === START GAME ========================================================
            game.RunGame();
        }
    }
}