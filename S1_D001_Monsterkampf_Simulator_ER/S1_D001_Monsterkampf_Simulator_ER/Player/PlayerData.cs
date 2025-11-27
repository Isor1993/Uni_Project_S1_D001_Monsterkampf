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

using S1_D001_Monsterkampf_Simulator_ER.Monsters;

namespace S1_D001_Monsterkampf_Simulator_ER.Player
{
    internal class PlayerData
    {
        // === Dependencies ===

        // === Fields ===
        public int UnassignedStatPoints { get; set; } = 0;

        public MonsterBase? ActiveMonster { get; set; }

        public int CompletedBattles { get; set; } = 0;
    }
}