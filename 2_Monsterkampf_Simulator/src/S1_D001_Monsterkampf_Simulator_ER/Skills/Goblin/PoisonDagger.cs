/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : PoisonDagger.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Goblin skill that performs a physical strike and applies a temporary
*   poison effect. The poison deals a percentage of MaxHP for several rounds.
*
* Responsibilities :
*   - Perform a basic AP-based physical strike
*   - Apply a PoisonEffect that deals % damage over time
*   - Run cooldown logic after use
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects;

namespace S1_D001_Monsterkampf_Simulator_ER.Skills.Goblin
{
    internal class PoisonDagger : SkillBase
    {
        // === Fields ===
        private const float SkillMultiplier = 0.10f;

        private const int SkillDuration = 2;
        private const int SkillCooldown = 1;
        private const float BasicDamage = 1f;

        /// <summary>
        /// Creates a new Poison Dagger skill instance.
        /// </summary>
        /// <param name="diagnostics">Diagnostics manager used for debug logging.</param>
        public PoisonDagger(DiagnosticsManager diagnostics) : base(
            "Poison Dagger",
            "Attack with poison. Inflict 10% poison damage for 2 rounds.",
            SkillType.Aktive,
            DamageType.Poison,
            BasicDamage,
            diagnostics)
        {
            Cooldown = SkillCooldown;
        }

        /// <summary>
        /// Calculates raw AP-based damage.
        /// </summary>
        /// <param name="attacker">The monster performing the attack.</param>
        /// <returns>Raw damage before reductions.</returns>
        public override float CalculateRawDamage(MonsterBase attacker)
        {
            float raw = attacker.Meta.AP * Power;
            _diagnostics.AddCheck($"{nameof(PoisonDagger)}.{nameof(CalculateRawDamage)}: RawDamage = {raw}.");
            return raw;
        }

        /// <summary>
        /// Applies a temporary PoisonEffect after the attack successfully hits.
        /// </summary>
        /// <param name="attacker">The monster performing the attack.</param>
        /// <param name="target">The monster receiving the poison effect.</param>
        public override void OnHit(MonsterBase attacker, MonsterBase target)
        {
            target.AddStatusEffect(new PoisonEffect(SkillDuration, SkillMultiplier, _diagnostics));
            _diagnostics.AddCheck($"{nameof(PoisonDagger)}.{nameof(OnHit)}: Applied PoisonEffect (10% for 2 rounds) on {target.Race}.");
        }
    }
}