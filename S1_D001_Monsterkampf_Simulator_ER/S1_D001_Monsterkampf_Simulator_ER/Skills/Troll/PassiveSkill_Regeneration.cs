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
    internal class PassiveSkill_Regeneration : SkillBase
    {
        // === Fields ===
        private const float Multiplier = 0.1f;

        public PassiveSkill_Regeneration(DiagnosticsManager diagnostics)
            : base(
                 "Regeneration",
                 "Regenerates 10% Health each round",
                 SkillType.Passive,
                 DamageType.None,
                 0f,
                 diagnostics)
        {
            Cooldown = 0;
        }

        public override void Apply(MonsterBase user, MonsterBase target)
        {
            user.AddStatusEffect(new RegenerationEffect(Multiplier, _diagnostics));
            _diagnostics.AddCheck($"{nameof(PassiveSkill_Regeneration)}.{nameof(Apply)}: Applied regeneration effect with multiplier {Multiplier}.");
        }
    }
}
