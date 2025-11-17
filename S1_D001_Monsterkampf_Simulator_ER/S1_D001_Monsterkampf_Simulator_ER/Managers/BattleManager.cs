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

using S1_D001_Monsterkampf_Simulator_ER.Dependencies;
using S1_D001_Monsterkampf_Simulator_ER.Systems.Damage;

namespace S1_D001_Monsterkampf_Simulator_ER.Managers
{
    internal class BattleManager
    {

        // === Dependencies ===
        private readonly BattleManagerDependencies _deps;
        // === Fields ===


        public BattleManager(BattleManagerDependencies deps)
        {
            _deps= deps;
        }

        public void RunBattle()
        {
            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}: Battle started between {_deps.Player.Race} and {_deps.Enemy.Race}.");

            _deps.Player.Spawn();
            _deps.Enemy.Spawn();

            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}: Both monsters spawned successfully.");
            int round = 1;
            _deps.Diagnostics.AddCheck($"{nameof(BattleManager)}: Starting Round {round}.");
        }

    }
}
