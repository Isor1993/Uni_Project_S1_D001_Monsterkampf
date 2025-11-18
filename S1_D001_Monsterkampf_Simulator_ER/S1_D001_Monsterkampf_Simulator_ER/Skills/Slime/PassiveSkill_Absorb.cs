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

namespace S1_D001_Monsterkampf_Simulator_ER.Skills.Slime
{
    internal class PassiveSkill_Absorb:SkillBase,IPassiveSkill
    {
        // === Dependencies ===

        // === Fields ===
        private const float SkillMultiplier = 0.3f;        
        private const int SkillCooldown = 0;
      


        public PassiveSkill_Absorb(DiagnosticsManager diagnostics)
            : base(
                 "Absorb",
                 "Absorbs 30% of all incoming damage.",
                 SkillType.Passive,
                 DamageType.None,
                 0f,
                 diagnostics)
        {
            Cooldown = SkillCooldown;
        }

        public  void ApplyPassive(MonsterBase user)
        {
            user.AddStatusEffect(new AbsorbEffect(SkillMultiplier,_diagnostics));

            _diagnostics.AddCheck($"{nameof(PassiveSkill_Absorb)}.{nameof(ApplyPassive)}: Applied absorb effect on {user.Race}.");

        }
    }

    
}
