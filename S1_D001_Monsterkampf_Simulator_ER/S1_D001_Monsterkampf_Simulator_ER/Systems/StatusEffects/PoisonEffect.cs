/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : PoisonEffect.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Status effect that applies poison damage at the end of each turn.
*   Damage is calculated as a percentage of the target’s maximum HP.
*
* Responsibilities :
*   - Apply poison damage at the end of each turn
*   - Calculate poison damage based on MaxHP and the multiplier
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;


namespace S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects
{
    internal class PoisonEffect : StatusEffectBase
    {
        private readonly float _multiplier;

        /// <summary>
        /// Creates a new poison status effect instance.
        /// </summary>
        /// <param name="duration">How many rounds the poison effect should last.</param>
        /// <param name="multiplier">Percentage of MaxHP dealt as poison damage each turn.</param>
        /// <param name="diagnostics">Diagnostics manager used for debug logging.</param>
        public PoisonEffect(int duration, float multiplier, DiagnosticsManager diagnostics)
            : base("Poison", duration, diagnostics)
        {
            _multiplier = multiplier;
        }

        /// <summary>
        /// Applies poison damage at the end of the target's turn.
        /// </summary>
        /// <param name="target">The monster receiving the poison damage.</param>
        public override void ApplyEndOfTurn(MonsterBase target)
        {
            float damage = target.Meta.MaxHP * _multiplier;
            damage = Math.Max(1, damage);
            target.TakeDamage(damage);
            _diagnostics.AddCheck($"{nameof(PoisonEffect)}.{nameof(ApplyEndOfTurn)}: {target.Race} took {damage} poison damage ({_multiplier * 100:F0}% of MaxHP).");
        }
    }
}