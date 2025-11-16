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

namespace S1_D001_Monsterkampf_Simulator_ER.Skills.Orc
{
    internal class PassiveSkill_Fear:SkillBase
    {
        // === Fields ===
        private const float SkillMultiplier = 0.5f;        
        private const int SkillCooldown = 0;

      
        public PassiveSkill_Fear(DiagnosticsManager diagnostics)
            : base(
                  "Fear",
                  "The enemy becomes frightened and loses 50% movement speed.",
                  SkillType.Passive,
                  DamageType.None,
                  0f,
                  diagnostics)
        {
            Cooldown = SkillCooldown;
        }

        /// <summary>
        /// Dieses Passive wird beim Spawn des Monsters angewendet.
        /// </summary>
        public void ApplyPassive(MonsterBase target)
        {
            target.AddStatusEffect(new FearEffect(SkillMultiplier, _diagnostics));
            _diagnostics.AddCheck($"{nameof(PassiveSkill_Fear)}.{nameof(ApplyPassive)}: Applied FearEffect (-50% speed) on {target.Race}.");
        }     
    }
}
