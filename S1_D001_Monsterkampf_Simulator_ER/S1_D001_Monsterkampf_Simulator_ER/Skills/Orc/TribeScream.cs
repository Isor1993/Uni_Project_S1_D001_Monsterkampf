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
        private const float Multiplier= 1.5f;

        public TribeScream(DiagnosticsManager diagnostics)
            :base(
                 "Tribe Scream",
                 "A scream of the OrcTribe wich gives you 50% more damage for 5 rounds.",
                 SkillType.Aktive,
                 DamageType.None,
                 0f,
                 diagnostics)
        {            
        }

        public override void Apply(MonsterBase user, MonsterBase target)
        {
            user.AddStatusEffect(new TribeScreamEffect(Multiplier,_diagnostics));
            _diagnostics.AddCheck($"{nameof(TribeScream)}.{nameof(Apply)}: Applied {Name} effect for 5 rouds .");
        }
        



    }
}
