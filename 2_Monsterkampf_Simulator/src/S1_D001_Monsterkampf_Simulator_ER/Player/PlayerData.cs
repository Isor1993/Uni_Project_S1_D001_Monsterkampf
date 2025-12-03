/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : PlayerData.cs
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
*   Stores persistent player-related data such as unassigned stat points
*   and completed battle count.
*
* Responsibilities :
*   - Track available stat points for allocation
*   - Track the number of completed battles
*
* History :
*   xx.xx.2025 ER Created
******************************************************************************/

namespace S1_D001_Monsterkampf_Simulator_ER.Player
{
    internal class PlayerData
    {
        // === Fields ===

        /// <summary>
        /// Available stat points the player can allocate to monsters.
        /// </summary>
        public int UnassignedStatPoints { get; set; } = 0;

        /// <summary>
        /// Number of battles successfully completed.
        /// </summary>
        public int CompletedBattles { get; set; } = 0;
    }
}