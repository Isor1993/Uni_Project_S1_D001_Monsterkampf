/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : KeyboardInputManager.cs
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
* Reads keyboard input from the user and translates it into PlayerCommand
* values for navigating menus, selecting skills and confirming actions.
*
* History :
* xx.xx.2025 ER Created
******************************************************************************/

using System;

namespace S1_D001_Monsterkampf_Simulator_ER.Managers
{
    internal class KeyboardInputManager : IPlayerInput
    {
        /// <summary>
        /// Reads a single key and returns the corresponding PlayerCommand.
        /// </summary>
        public PlayerCommand ReadCommand()
        {
            ConsoleKeyInfo key = Console.ReadKey(intercept: true);

            return key.Key switch
            {
                ConsoleKey.UpArrow => PlayerCommand.MoveUp,
                ConsoleKey.DownArrow => PlayerCommand.MoveDown,
                ConsoleKey.Enter => PlayerCommand.Confirm,

                // Everything else returns "None"
                _ => PlayerCommand.None
            };
        }
    }
}