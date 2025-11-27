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

        public override float CalculateRawDamage(MonsterBase attacker)
        {
            float raw = attacker.Meta.AP * Power;
            _diagnostics.AddCheck($"{nameof(PoisonDagger)}.{nameof(CalculateRawDamage)}: RawDamage = {raw}.");
            return raw;
        }

        public override void OnHit(MonsterBase attacker, MonsterBase target)
        {
            target.AddStatusEffect(new PoisonEffect(
                duration: SkillDuration,
                multiplier: SkillMultiplier,
                _diagnostics));

            _diagnostics.AddCheck($"{nameof(PoisonDagger)}.{nameof(OnHit)}: Applied PoisonEffect (10% for 2 rounds) on {target.Race}.");
        }
    }
}