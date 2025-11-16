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
using S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects;

namespace S1_D001_Monsterkampf_Simulator_ER.Skills.Orc
{
    
    internal class TribeScream:SkillBase
    {

        // === Fields ===
        private const float SkillMultiplier = 1.5f;
        private const int SkillDuration = 5;
        private const int SkillCooldown = 5;
        

        public TribeScream(DiagnosticsManager diagnostics)
            :base(
                 "Tribe Scream",
                 "A scream of the Orc tribe that grants you 50% more damage for 5 rounds.",
                 SkillType.Aktive,
                 DamageType.None,
                 0f,
                 diagnostics)
        {
            Cooldown = SkillCooldown;
        }
        public override void OnCast(MonsterBase caster)
        {
            caster.AddStatusEffect(new TribeScreamEffect(
                multiplier: SkillMultiplier,
                duration: SkillDuration,
                diagnostics: _diagnostics));

            _diagnostics.AddCheck($"{nameof(TribeScream)}.{nameof(OnCast)}: Applied AP buff (+50%) for {SkillDuration} rounds to {caster.Race}.");
        }
    }
}
