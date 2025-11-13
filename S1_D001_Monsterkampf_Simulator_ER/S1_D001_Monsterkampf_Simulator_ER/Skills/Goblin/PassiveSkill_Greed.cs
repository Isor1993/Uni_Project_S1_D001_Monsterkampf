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

namespace S1_D001_Monsterkampf_Simulator_ER.Skills.Goblin
{
    internal class PassiveSkill_Greed:SkillBase
    {

        // === Fields ===
        private const float RewardMultiplier = 1.5f;
        

        public PassiveSkill_Greed() 
            : base(
            "Greed",
            "You get 50% more reward after a won fight.",
            SkillType.Meta,
            DamageType.None,
            0f)
        {            
        }

        public float ApplyRewardBonus(float baseReward)
        {
            return baseReward * RewardMultiplier;
        }

    }
}
