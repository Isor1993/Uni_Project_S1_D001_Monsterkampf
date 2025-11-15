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
    internal class PassiveSkill_Absorb:SkillBase
    {
        // === Dependencies ===

        // === Fields ===
        private const float _percent = 0.3f;



        public PassiveSkill_Absorb(DiagnosticsManager diagnostics)
            : base(
                 "Absorb",
                 "Absorbs 30% of all incoming damage.",
                 SkillType.Passive,
                 DamageType.None,
                 0f,
                 diagnostics)
        {
            Cooldown = 0;
        }

        public override void Apply(MonsterBase user, MonsterBase target)
        {
            user.AddStatusEffect(new AbsorbEffect(_percent,_diagnostics));

            _diagnostics.AddCheck($"{nameof(PassiveSkill_Absorb)}.{nameof(Apply)}: Applied absorb effect.");

        }
    }

    
}
