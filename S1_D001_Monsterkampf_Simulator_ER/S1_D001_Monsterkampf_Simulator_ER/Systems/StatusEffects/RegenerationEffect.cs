/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : RegenerationEffect.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Permanent status effect that regenerates a percentage of the monster's
*   maximum HP at the start of each turn.
*
* Responsibilities :
*   - Regenerate HP every turn
*   - Calculate regeneration based on MaxHP and the multiplier
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;

namespace S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects
{
    
    internal class RegenerationEffect : PermanentStatusEffect
    {
        private readonly float _multiplier;

        /// <summary>
        /// Creates a new regeneration effect instance.
        /// </summary>
        /// <param name="multiplier">Percentage of MaxHP restored each turn.</param>
        /// <param name="diagnostics">Diagnostics manager used for debug logging.</param>
        public RegenerationEffect(float multiplier, DiagnosticsManager diagnostics)
            : base("Regeneration", diagnostics)
        {
            _multiplier = multiplier;
        }

        /// <summary>
        /// Regenerates HP based on MaxHP and the multiplier at the start of each turn.
        /// </summary>
        /// <param name="target">The monster receiving the regeneration effect.</param>
        public override void ApplyStartOfTurn(MonsterBase target)
        {
            float regeneration = target.Meta.MaxHP * _multiplier;
            regeneration = Math.Max(1, regeneration);
            target.Heal(regeneration);
            _diagnostics.AddCheck($"{nameof(RegenerationEffect)}.{nameof(ApplyStartOfTurn)}: {target.Race} regenerated {regeneration} HP ({_multiplier * 100:F0}%).");
        }
    }
}
