/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : IPlayerInput.cs
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
* Defines player input commands and the interface for reading input during
* battles (skill navigation, confirmations, etc.).
*
* History :
* xx.xx.2025 ER Created
******************************************************************************/

namespace S1_D001_Monsterkampf_Simulator_ER.Managers
{
    /// <summary>
    /// Possible commands the player can execute when navigating the UI.
    /// </summary>
    public enum PlayerCommand
    {
        None,
        MoveUp,
        MoveDown,
        Confirm
    }

    /// <summary>
    /// Defines the interface for reading player input (keyboard or other devices).
    /// </summary>
    public interface IPlayerInput
    {
        /// <summary>
        /// Reads and returns the next player command.
        /// </summary>
        PlayerCommand ReadCommand();
    }
    
    }