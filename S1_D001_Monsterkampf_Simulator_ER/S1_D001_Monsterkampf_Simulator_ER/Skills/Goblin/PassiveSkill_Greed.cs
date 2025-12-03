/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : PassiveSkill_Greed.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Passive/meta skill used by the Goblin. Increases the reward gained after
*   winning a battle without affecting combat directly.
*
* Responsibilities :
*   - Modify the victory reward after a won fight
*   - Provide debug information about reward changes
*
* History :
*   03.12.2025 ER Created
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

        /// <summary>
        /// Creates a new Greed passive skill instance.
        /// </summary>
        /// <param name="diagnostics">Diagnostics manager used for debug logging.</param>
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
        /// Called when the passive is initialized.
        /// Greed has no immediate effect on spawn, only on rewards after victory.
        /// </summary>
        /// <param name="user">The monster owning the Greed passive.</param>
        public void ApplyPassive(MonsterBase user)
        {
            _diagnostics.AddCheck($"{nameof(PassiveSkill_Greed)}.{nameof(ApplyPassive)}: No immediate spawn effect. Greed will modify rewards after victory.");
        }

        /// <summary>
        /// Modifies the victory reward by applying the Greed multiplier.
        /// </summary>
        /// <param name="baseReward">Base reward before modification.</param>
        /// <returns>Final reward value after applying the multiplier.</returns>
        public override float ModifyVictoryReward(float baseReward)
        {
            float finalReward = baseReward * SkillMultiplier;
            _diagnostics.AddCheck($"{nameof(PassiveSkill_Greed)}.{nameof(ModifyVictoryReward)}: BaseReward={baseReward} → FinalReward={finalReward} (+50%).");

            return finalReward;
        }
    }
}