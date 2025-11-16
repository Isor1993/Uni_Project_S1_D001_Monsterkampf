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
        private readonly float _multiplier;


        
        public AbsorbEffect(float multiplier, DiagnosticsManager diagnostics)
            : base(
                 "Absorb",
                 diagnostics)
        {
            _multiplier = multiplier;
        }

        /// <summary> 
        /// Calculates the final damage after applying the absorb reduction.
        /// Example: 30% absorb → finalDamage = incomingDamage * 0.7
        /// </summary>
        public float AbsorbDamage(float incomingDamage)
        {
            float finalDamage = Math.Max(1,incomingDamage*(1f-_multiplier));
            float reducedAmount = incomingDamage-finalDamage;
            
            

            _diagnostics.AddCheck($"{nameof(AbsorbEffect)}.{nameof(AbsorbDamage)}: Incoming = {incomingDamage}, Reduced = {reducedAmount} ({_multiplier * 100:F0}%), Final = {finalDamage}.");

            return finalDamage;
        }
    }
}
