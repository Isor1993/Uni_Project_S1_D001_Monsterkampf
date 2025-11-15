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
using S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects;

namespace S1_D001_Monsterkampf_Simulator_ER.Skills.Goblin
{
    internal class PassiveSkill_Greed : SkillBase
    {

        // === Fields ===
        private const float Multiplier = 1.5f;


        public PassiveSkill_Greed(DiagnosticsManager diagnostics)
            : base(
            "Greed",
            "You get 50% more reward after a won fight.",
            SkillType.Meta,
            DamageType.None,
            0f,
            diagnostics)
        {
            Cooldown = 0;
        }

        public float ApplyRewardBonus(float baseReward)
        {
            baseReward = baseReward * Multiplier;
            _diagnostics.AddCheck($"{nameof(PassiveSkill_Greed)}.{nameof(ApplyRewardBonus)}: Applied multiplier {Multiplier} to base Reward.");
            return baseReward;
        }

    }
}
