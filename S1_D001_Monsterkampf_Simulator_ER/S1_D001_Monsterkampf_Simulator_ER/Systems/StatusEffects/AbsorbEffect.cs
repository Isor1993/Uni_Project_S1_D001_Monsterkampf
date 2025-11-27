/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : AbsorbEffect.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Permanent status effect that reduces incoming damage by a percentage.
*   This effect is part of the Slime’s passive skill set and applies to all
*   incoming damage calculations.
*
* Responsibilities :
*   - Reduce incoming damage by a multiplier
*   - Ensure the damage does not fall below a minimum value
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;

namespace S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects
{
    internal class AbsorbEffect : PermanentStatusEffect
    {
        // === Fields ===
        private readonly float _multiplier;

        /// <summary>
        /// Creates a new absorb status effect instance.
        /// </summary>
        /// <param name="multiplier">Percentage of incoming damage absorbed.</param>
        /// <param name="diagnostics">Diagnostics manager used for debug logging.</param>
        public AbsorbEffect(float multiplier, DiagnosticsManager diagnostics)
            : base("Absorb", diagnostics)
        {
            _multiplier = multiplier;
        }

        /// <summary>
        /// Reduces the incoming damage based on the multiplier.
        /// Ensures a minimum of 1 damage is always dealt.
        /// </summary>
        /// <param name="incomingDamage">Raw incoming damage before absorption.</param>
        /// <returns>The final reduced damage value.</returns>
        public float AbsorbDamage(float incomingDamage)
        {
            float finalDamage = Math.Max(1, incomingDamage * (1f - _multiplier));
            float reducedAmount = incomingDamage - finalDamage;

            _diagnostics.AddCheck($"{nameof(AbsorbEffect)}.{nameof(AbsorbDamage)}: Incoming = {incomingDamage}, Reduced = {reducedAmount} ({_multiplier * 100:F0}%), Final = {finalDamage}."
            return finalDamage;
        }
    }
}
