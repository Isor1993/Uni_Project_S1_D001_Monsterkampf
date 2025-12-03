/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : TribeScream.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Active buff skill used by the Orc. Applies a temporary AP multiplier that
*   increases physical damage output for a limited duration.
*
* Responsibilities :
*   - Apply a timed AP buff to the caster
*   - Trigger a status effect that lasts several rounds
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects;

namespace S1_D001_Monsterkampf_Simulator_ER.Skills.Orc
{
    internal class TribeScream : SkillBase
    {
        // === Fields ===
        private const float SkillMultiplier = 1.5f;

        private const int SkillDuration = 5;
        private const int SkillCooldown = 5;
        private const float BasicDamage = 0.5f;

        /// <summary>
        /// Creates a new Tribe Scream skill instance.
        /// </summary>
        /// <param name="diagnostics">Diagnostics manager used for debug logging.</param>
        public TribeScream(DiagnosticsManager diagnostics)
            : base(
                 "Tribe Scream",
                 "A buff which grants 50% more damage for 5 rounds.",
                 SkillType.Aktive,
                 DamageType.None,
                 BasicDamage,
                 diagnostics)
        {
            Cooldown = SkillCooldown;
        }

        /// <summary>
        /// Applies the AP buff to the caster by adding a TribeScreamEffect instance.
        /// </summary>
        /// <param name="caster">The monster receiving the AP buff.</param>
        public override void OnCast(MonsterBase caster)
        {
            caster.AddStatusEffect(new TribeScreamEffect(multiplier: SkillMultiplier, duration: SkillDuration, diagnostics: _diagnostics));
            _diagnostics.AddCheck($"{nameof(TribeScream)}.{nameof(OnCast)}: Applied AP buff (+50%) for {SkillDuration} rounds to {caster.Race}.");
        }
    }
}