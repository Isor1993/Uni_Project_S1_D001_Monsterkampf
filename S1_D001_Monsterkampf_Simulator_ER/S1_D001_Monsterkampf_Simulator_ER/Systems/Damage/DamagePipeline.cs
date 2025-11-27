/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    :
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
* *
* History :
* xx.xx.2025 ER Created
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

        // === Fields ===

        public DamagePipeline(DiagnosticsManager diagnostics)
        {
            _diagnostics = diagnostics;
        }

        /// <summary>
        /// Führt die komplette Damage-Pipeline aus:
        /// 1. OnCast
        /// 2. RawDamage
        /// 3. Defense
        /// 4. Resistance
        /// 5. Absorb/Schilde/Thorns
        /// 6. target.TakeDamage
        /// 7. OnHit
        /// </summary>
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