/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : PrintManager.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Simple helper manager responsible for writing formatted or unformatted 
*   text to the console. Mainly used for generic printing, debug text, 
*   and keeping responsibilities separated from core game logic.
*
* Responsibilities :
*   - Provide a clean wrapper for Console.WriteLine output
*   - Act as a future extension point for printing (colors, formatting, logs)
*   - Optionally integrate diagnostics for development checks
*
* History :
*   03.12.2025 ER Created
******************************************************************************/
namespace S1_D001_Monsterkampf_Simulator_ER.Managers
{
    internal class PrintManager
    {
        private readonly DiagnosticsManager diagnostics;

        /// <summary>
        /// Creates a new PrintManager used to print simple text lines
        /// to the console. Mostly intended for general-purpose output,
        /// debug messages, and future formatting extensions.
        /// </summary>
        /// <param name="diagnostics">
        /// DiagnosticsManager used for potential logging or validation.
        /// </param>
        public PrintManager(DiagnosticsManager diagnostics)
        {
            this.diagnostics = diagnostics;
        }

        /// <summary>
        /// Prints a single message as a console line. No extra formatting,
        /// simply writes the text as-is.
        /// </summary>
        /// <param name="message">
        /// The string to print to the console output.
        /// </param>
        public void Line(string message)
        {
            Console.WriteLine($"{message}");
        }
    }
}
