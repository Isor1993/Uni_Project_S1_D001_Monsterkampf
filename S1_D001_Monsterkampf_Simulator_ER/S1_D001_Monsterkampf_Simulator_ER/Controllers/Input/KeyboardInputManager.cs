/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : KeyboardInputManager.cs
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
* Handles player input for menu-like selections (W/S navigation and number keys).
*
* History :
* xx.xx.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;

namespace S1_D001_Monsterkampf_Simulator_ER.Controllers.Input
{
    internal class KeyboardInputManager : IPlayerInput
    {
        // === Dependencies ===
        private readonly DiagnosticsManager _diagnostics;

        public KeyboardInputManager(DiagnosticsManager diagnostics)
        {
            _diagnostics = diagnostics ?? throw new ArgumentNullException(nameof(diagnostics));
        }

        public int ChooseIndex(string[] options)
        {
            if (options == null || options.Length == 0)
            {
                throw new ArgumentException("Option must not be null or empty.", nameof(options));
            }
            int selectedIndex = 0;

            while (true)
            {
                //TODO verbindung zu Ui manager
                PrintOptions(options, selectedIndex);

                ConsoleKeyInfo key = Console.ReadKey(intercept: true);

                switch (key.Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                        break;

                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex + 1) % options.Length;
                        break;

                    case ConsoleKey.Enter:
                        _diagnostics.AddCheck($"{nameof(KeyboardInputManager)}.{nameof(ChooseIndex)}: Player confirmed index {selectedIndex} ('{options[selectedIndex]}').");
                        return selectedIndex;

                    default:

                        if (char.IsDigit(key.KeyChar))
                        {
                            int digit = key.KeyChar - '1';

                            if (digit >= 0 && digit < options.Length)
                            {
                                _diagnostics.AddCheck($"{nameof(KeyboardInputManager)}.{nameof(ChooseIndex)}: Player chose index {digit} via number key ('{options[digit]}').");
                                return digit;
                            }
                        }
                        Console.Beep();
                        break;
                }
            }
        }

        //TODO Später richtig machen das ist dur für testswecke
        private void PrintOptions(string[] options, int selectedIndex)
        {
            Console.Clear();
            Console.WriteLine("Choose action:\n");

            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.WriteLine($"> {options[i]} <");
                }
                else
                {
                    Console.WriteLine($"  {options[i]}");
                }
            }
        }
    }
}
