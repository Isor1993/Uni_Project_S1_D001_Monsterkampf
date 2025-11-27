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

namespace S1_D001_Monsterkampf_Simulator_ER.Skills.Slime
{
    internal class Fireball : SkillBase
    {
        // === Dependencies ===

        // === Fields ===
        private const float SkillMultiplier = 1.5f;

        private const int SkillCooldown = 2;

        public Fireball(DiagnosticsManager diagnostics)
            : base(
                 "Fireball",
                 "Shoots a fireball which deals 50% more damage.",
                 SkillType.Aktive,
                 DamageType.Fire,
                 SkillMultiplier,
                 diagnostics)
        {
            Cooldown = SkillCooldown;
        }

        public override float CalculateRawDamage(MonsterBase attacker)
        {
            float raw = attacker.Meta.AP * Power;
            _diagnostics.AddCheck($"{nameof(Fireball)}.{nameof(CalculateRawDamage)}: RawDamage = {raw}.");
            return raw;
        }
    }
}