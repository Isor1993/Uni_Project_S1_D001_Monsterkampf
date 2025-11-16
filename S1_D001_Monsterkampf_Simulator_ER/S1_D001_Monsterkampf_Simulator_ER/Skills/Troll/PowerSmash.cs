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
using S1_D001_Monsterkampf_Simulator_ER.Skills.Goblin;

namespace S1_D001_Monsterkampf_Simulator_ER.Skills.Troll
{
    internal class PowerSmash:SkillBase
    {
        private const float SkillMultiplier = 1.8f;        
        private const int SkillCooldown = 3;
        public PowerSmash(DiagnosticsManager diagnostics)
       : base(
            "Power Smash",
            "Unleashes a powerful smash, dealing +80% physical damage",
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
            _diagnostics.AddCheck($"{nameof(PowerSmash)}.{nameof(CalculateRawDamage)}: RawDamage = {raw}.");
            return raw;
        }
       

    }
}
