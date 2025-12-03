/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : IPassiveSkill.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Interface for passive skills that are automatically applied at battle start.
*   Passive skills modify monster attributes, apply permanent effects, or
*   influence rewards without requiring player activation.
*
* Responsibilities :
*   - Provide a unified method for applying passive effects
*   - Ensure consistent handling of passive skill initialization
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Monsters;

namespace S1_D001_Monsterkampf_Simulator_ER.Skills
{
    internal interface IPassiveSkill
    {
        /// <summary>
        /// Applies the passive effect to the owning monster.
        /// Called once at the start of each battle.
        /// </summary>
        /// <param name="owner">The monster receiving the passive effect.</param>
        void ApplyPassive(MonsterBase owner);
    }
}