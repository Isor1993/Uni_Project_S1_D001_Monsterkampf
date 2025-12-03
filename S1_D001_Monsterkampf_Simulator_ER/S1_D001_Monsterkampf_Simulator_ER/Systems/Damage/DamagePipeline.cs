/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : DamagePipeline.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Handles the complete damage calculation process for executed skills.
*   Executes casting behavior, raw damage calculation, defense, resistance,
*   absorb effects, final modifications and applying the final damage value.
*
* Responsibilities :
*   - Execute the full damage calculation pipeline
*   - Apply defense, resistances and absorb effects
*   - Invoke skill OnCast and OnHit behavior
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Skills;
using S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects;

namespace S1_D001_Monsterkampf_Simulator_ER.Systems.Damage
{
    internal class DamagePipeline
    {
        // === Dependencies ===
        private readonly DiagnosticsManager _diagnostics;

        /// <summary>
        /// Creates a new damage pipeline instance.
        /// </summary>
        /// <param name="diagnostics">Diagnostics manager used for debug logging.</param>
        public DamagePipeline(DiagnosticsManager diagnostics)
        {
            _diagnostics = diagnostics;
        }

        /// <summary>
        /// Executes the complete damage pipeline:
        /// Executes the complete damage pipeline:
        /// 1. OnCast
        /// 2. RawDamage calculation
        /// 3. Defense reduction
        /// 4. Resistance reduction
        /// 5. Absorb effects
        /// 6. Final damage modifications
        /// 7. Apply damage to target
        /// 8. OnHit
        /// </summary>
        /// <param name="attacker">The monster performing the attack.</param>
        /// <param name="target">The monster receiving the damage.</param>
        /// <param name="skill">The skill used for the attack.</param>
        /// <returns>The final damage dealt.</returns>
        public float Execute(MonsterBase attacker, MonsterBase target, SkillBase skill)
        {
            _diagnostics.AddCheck($"{nameof(DamagePipeline)}: Starting DamagePipeline for skill '{skill.Name}'.");

            skill.OnCast(attacker);
            _diagnostics.AddCheck($"{nameof(DamagePipeline)}: OnCast executed for '{skill.Name}'.");
            float rawDamage = skill.CalculateRawDamage(attacker);
            _diagnostics.AddCheck($"{nameof(DamagePipeline)}: RawDamage = {rawDamage}.");
            float afterDefense = rawDamage - target.Meta.DP;
            afterDefense = Math.Max(1, afterDefense);

            _diagnostics.AddCheck($"{nameof(DamagePipeline)}: AfterDefense = {afterDefense} (DP={target.Meta.DP}).");
            float afterResistance = GetAfterResistance(target, skill, afterDefense);
            float afterAbsorb = ApplyAbsorb(target, afterResistance);
            float afterModify = target.ModifyFinalDamage(afterAbsorb);
            float finalDamage = afterModify;

            target.TakeDamage(finalDamage);
            _diagnostics.AddCheck($"{nameof(DamagePipeline)}: {target.Race} took {finalDamage} damage.");
            skill.OnHit(attacker, target);
            _diagnostics.AddCheck($"{nameof(DamagePipeline)}: OnHit executed for '{skill.Name}'.");

            return finalDamage;
        }

        /// <summary>
        /// Applies elemental resistance reduction based on the skill's damage type.
        /// </summary>
        /// <param name="target">The monster receiving the damage.</param>
        /// <param name="skill">The skill determining the damage type.</param>
        /// <param name="afterDefense">Damage value after applying defense.</param>
        /// <returns>Damage after applying resistance.</returns>
        private float GetAfterResistance(MonsterBase target, SkillBase skill, float afterDefense)
        {
            float resistance = skill.DamageType switch
            {
                DamageType.Physical => target.Resistance.Physical,
                DamageType.Fire => target.Resistance.Fire,
                DamageType.Water => target.Resistance.Water,
                DamageType.Poison => target.Resistance.Poison,
                _ => 0f
            };

            float afterResistance = afterDefense * (1f - resistance);
            afterResistance = Math.Max(1, afterResistance);

            _diagnostics.AddCheck($"{nameof(DamagePipeline)}.{nameof(GetAfterResistance)}: AfterResistance = {afterResistance} (Res={resistance}).");

            return afterResistance;
        }

        /// <summary>
        /// Applies absorb effects if the target has an active AbsorbEffect.
        /// </summary>
        /// <param name="target">The monster that may absorb damage.</param>
        /// <param name="damage">Damage value before absorb.</param>
        /// <returns>Damage after applying absorb.</returns>
        private float ApplyAbsorb(MonsterBase target, float damage)
        {
            var absorb = target.GetStatusEffects<AbsorbEffect>().FirstOrDefault();
            if (absorb == null)
            {
                return damage;
            }
            float reduced = absorb.AbsorbDamage(damage);
            reduced = Math.Max(1, reduced);

            _diagnostics.AddCheck($"{nameof(DamagePipeline)}.{nameof(ApplyAbsorb)}: Absorb applied → {damage} → {reduced}");
            return reduced;
        }
    }
}