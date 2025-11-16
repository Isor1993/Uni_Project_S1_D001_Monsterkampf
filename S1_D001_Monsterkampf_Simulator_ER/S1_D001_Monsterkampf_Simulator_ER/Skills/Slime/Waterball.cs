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


namespace S1_D001_Monsterkampf_Simulator_ER.Skills.Slime
{
    internal class Waterball:SkillBase
    {
        // === Dependencies ===

        // === Fields ===
        private const float SkillMultiplier = 1.5f;      
        private const int SkillCooldown = 2;



        public Waterball(DiagnosticsManager diagnostics)
            : base(
                 "Waterball",
                 "Shoots a waterball which deals 50% more damage.",
                 SkillType.Aktive,
                 DamageType.Water,
                 SkillMultiplier,
                 diagnostics)
        {
            Cooldown = SkillCooldown;
        }
        public override float CalculateRawDamage(MonsterBase attacker)
        {
            float raw = attacker.Meta.AP * Power;
            _diagnostics.AddCheck($"{nameof(Waterball)}.{nameof(CalculateRawDamage)}: RawDamage = {raw}.");
            return raw;
        }
       
    }
}
