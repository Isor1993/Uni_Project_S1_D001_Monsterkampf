/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : PlayerCommand.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
* Defines all possible UI navigation commands the player can trigger, and the
* input interface used by keyboard and future input devices.
*
* History :
* 03.12.2025 ER Created
******************************************************************************/

namespace S1_D001_Monsterkampf_Simulator_ER.Managers
{
    /// <summary>
    /// Represents all player navigation commands used in menus and battle UI.
    /// </summary>
    public enum PlayerCommand
    {
        None,
        MoveUp,
        MoveDown,
        Confirm
    }

    /// <summary>
    /// Interface for reading player input (keyboard or alternative controls).
    /// </summary>
    public interface IPlayerInput
    {
        /// <summary>
        /// Reads and returns the next player command.
        /// </summary>
        PlayerCommand ReadCommand();
    }

}