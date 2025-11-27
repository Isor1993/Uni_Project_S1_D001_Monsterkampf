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

namespace S1_D001_Monsterkampf_Simulator_ER.Skills.Goblin
{
    internal class PassiveSkill_Greed : SkillBase
    {
        // === Fields ===
        private const float SkillMultiplier = 1.5f;

        private const int SkillCooldown = 0;

        public PassiveSkill_Greed(DiagnosticsManager diagnostics)
            : base(
            "Greed",
            "You get 50% more reward after a won fight.",
            SkillType.Meta,
            DamageType.None,
            0f,
            diagnostics)
        {
            Cooldown = SkillCooldown;
        }

        /// <summary>
        /// Einheitliche Passive-Schnittstelle für alle passiven Skills.
        /// Greed hat keinen sofortigen Effekt beim Spawn.
        /// </summary>
        public void ApplyPassive(MonsterBase user)
        {
            _diagnostics.AddCheck($"{nameof(PassiveSkill_Greed)}.{nameof(ApplyPassive)}: No immediate spawn effect. Greed will modify rewards after victory.");
        }

        /// <summary>
        /// Der eigentliche Reward-Bonus: +50% Reward nach gewonnenem Kampf.
        /// </summary>
        public override float ModifyVictoryReward(float baseReward)
        {
            float finalReward = baseReward * SkillMultiplier;

            _diagnostics.AddCheck($"{nameof(PassiveSkill_Greed)}.{nameof(ModifyVictoryReward)}: BaseReward={baseReward} → FinalReward={finalReward} (+50%).");

            return finalReward;
        }
    }
}