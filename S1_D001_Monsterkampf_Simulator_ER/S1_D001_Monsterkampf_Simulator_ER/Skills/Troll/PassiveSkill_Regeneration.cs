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

namespace S1_D001_Monsterkampf_Simulator_ER.Skills.Troll
{
    internal class PassiveSkill_Regeneration : SkillBase,IPassiveSkill
    {
        // === Fields ===
        
        private const float SkillMultiplier = 0.0005f;       
        private const int SkillCooldown = 0;


        public PassiveSkill_Regeneration(DiagnosticsManager diagnostics)
            : base(
                 "Regeneration",
                 "Regenerates 10% of Max HP each round",
                 SkillType.Passive,
                 DamageType.None,
                 0f,
                 diagnostics)
        {
            Cooldown = SkillCooldown;
        }

        public  void ApplyPassive(MonsterBase user)
        {
            user.AddStatusEffect(new RegenerationEffect(SkillMultiplier, _diagnostics));
            _diagnostics.AddCheck($"{nameof(PassiveSkill_Regeneration)}.{nameof(ApplyPassive)}: Applied regeneration effect (+10% Health) on {user.Race}.");
        }
    }
}
