/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : BasicAttack.cs
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
* Represents a simple, standard AP-based attack with no cooldown.
*
* History :
* xx.xx.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;

namespace S1_D001_Monsterkampf_Simulator_ER.Skills
{
    internal class BasicAttack : SkillBase
    {
        private const float SkillMultiplier = 1.0f;
        private const int SkillDuration = 0;
        private const int SkillCooldown = 0;

        public BasicAttack(DiagnosticsManager diagnostics)
            : base(
                  "Basic Attack",
                  "A standard physical attack based on AP.",
                  SkillType.Aktive,
                  DamageType.Physical,
                  SkillMultiplier,
                  diagnostics
                  )
        {
            Cooldown = SkillCooldown;
            CurrentCooldown = SkillDuration;
        }

        public override float CalculateRawDamage(MonsterBase attacker)
        {
            float raw = attacker.Meta.AP * Power;
            _diagnostics.AddCheck($"{nameof(BasicAttack)}.{nameof(CalculateRawDamage)}: RawDamage = {raw}.");
            return raw;
        }
    }
}