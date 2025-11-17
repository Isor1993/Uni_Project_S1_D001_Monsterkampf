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

namespace S1_D001_Monsterkampf_Simulator_ER.Controllers.Input
{
    internal interface IPlayerInput
    {
        /// <summary>
        /// Lets the user select an index from a list of options.
        /// Returns an integer between 0 and options.Length - 1.
        /// </summary>
        int ChooseIndex(string[] options);
    }
}
