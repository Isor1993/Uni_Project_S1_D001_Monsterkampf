/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : PermanentStatusEffect.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Base class for status effects that do not expire naturally.
*   Permanent effects have an infinite duration and do not tick down.
*
* Responsibilities :
*   - Represent a status effect with infinite duration
*   - Prevent ticking behavior for permanent effects
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;

namespace S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects
{
    internal abstract class PermanentStatusEffect : StatusEffectBase
    {
        /// <summary>
        /// Creates a new permanent status effect instance.
        /// </summary>
        /// <param name="name">The name of the status effect.</param>
        /// <param name="diagnostics">Diagnostics manager used for debug logging.</param>
        protected PermanentStatusEffect(string name, DiagnosticsManager diagnostics)
            : base(name, int.MaxValue, diagnostics) // infinite duration
        {
        }

        /// <summary>
        /// Overrides normal duration ticking for permanent effects.
        /// A permanent effect does not decrease duration.
        /// </summary>
        public override void Tick()
        {
            _diagnostics.AddCheck($"{nameof(PermanentStatusEffect)}.{nameof(Tick)}: Permanent status does not tick.");
        }
    }
}