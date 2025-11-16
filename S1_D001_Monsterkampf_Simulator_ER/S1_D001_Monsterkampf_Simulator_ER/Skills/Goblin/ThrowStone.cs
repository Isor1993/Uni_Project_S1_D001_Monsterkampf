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

namespace S1_D001_Monsterkampf_Simulator_ER.Skills.Goblin
{
    internal class ThrowStone : SkillBase
    {
        private const float SkillMultiplier = 1.3f;        
        private const int SkillCooldown = 1;

        public ThrowStone(DiagnosticsManager diagnostics)
        : base(
             "ThrowStone",
             "Throw a stone and deal 30% more damage.",
             SkillType.Aktive,
             DamageType.Physical,
             SkillMultiplier,
             diagnostics)
        {
            Cooldown = SkillCooldown;
        }
        public override float CalculateRawDamage(MonsterBase attacker)
        {
            float raw = attacker.Meta.AP * Power;
            _diagnostics.AddCheck($"{nameof(ThrowStone)}.{nameof(CalculateRawDamage)}: RawDamage = {raw}.");
            return raw;
        }             
    }
}
