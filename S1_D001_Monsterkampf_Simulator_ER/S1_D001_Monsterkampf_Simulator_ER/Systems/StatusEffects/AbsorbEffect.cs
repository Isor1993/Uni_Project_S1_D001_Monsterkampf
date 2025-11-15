/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : AbsorbEffect.cs
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
* Reduces incoming damage by a percentage. This effect is permanent
* and part of the Slime’s passive skills.
*
* History :
* xx.xx.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using System.Diagnostics.Tracing;

namespace S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects
{
    internal class AbsorbEffect : PermanentStatusEffect
    {

        // === Fields ===
        private readonly float _percent;


        
        public AbsorbEffect(float percent, DiagnosticsManager diagnostics)
            : base(
                 "Absorb",
                 diagnostics)
        {
            _percent = percent;
        }

        /// <summary> 
        /// Calculates the final damage after applying the absorb reduction.
        /// Example: 30% absorb → finalDamage = incomingDamage * 0.7
        /// </summary>
        public float AbsorbDamage(float incomingDamage)
        {
            float reducedAmount = incomingDamage * _percent;
            float finalDamage = incomingDamage - reducedAmount;
            finalDamage = Math.Max(1, finalDamage);

            _diagnostics.AddCheck($"{nameof(AbsorbEffect)}.{nameof(AbsorbDamage)}: Incoming = {incomingDamage}, Reduced = {reducedAmount} ({_percent * 100:F0}%), Final = {finalDamage}.");

            return finalDamage;
        }
    }
}
