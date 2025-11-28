/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : KeyboardInputManager.cs
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
* Provides keyboard-based player input handling.
* Translates ConsoleKey input into PlayerCommand actions 
* (MoveUp, MoveDown, Confirm).
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
                ConsoleKey.W => PlayerCommand.MoveUp,
                ConsoleKey.DownArrow => PlayerCommand.MoveDown,
                ConsoleKey.S => PlayerCommand.MoveDown,
                ConsoleKey.Enter => PlayerCommand.Confirm,

                _ => PlayerCommand.None
            };
        }
    }
}