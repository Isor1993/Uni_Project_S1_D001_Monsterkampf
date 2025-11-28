/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : PassiveSkill_Fear.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Passive skill used by the Orc. Applies a permanent FearEffect to the enemy,
*   reducing its Speed stat by 50%. This debuff persists for the entire battle.
*
* Responsibilities :
*   - Apply the FearEffect once at battle start
*   - Reduce the enemy’s Speed via a permanent status effect
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects;
namespace S1_D001_Monsterkampf_Simulator_ER.Skills.Orc
{
    internal class PassiveSkill_Fear : SkillBase, IPassiveSkill
    {
        // === Fields ===
        private const float SkillMultiplier = 0.5f;
        private const int SkillCooldown = 0;

        /// <summary>
        /// Creates a new Fear passive skill instance.
        /// </summary>
        /// <param name="diagnostics">Diagnostics manager used for debug logging.</param>
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
        /// Applies the passive FearEffect to the target.
        /// Called automatically at the start of each battle.
        /// </summary>
        /// <param name="target">The monster receiving the Fear debuff.</param>
        public void ApplyPassive(MonsterBase target)
        {
            target.AddStatusEffect(new FearEffect(SkillMultiplier, _diagnostics));
            _diagnostics.AddCheck($"{nameof(PassiveSkill_Fear)}.{nameof(ApplyPassive)}: Applied FearEffect (-50% speed) on {target.Race}.");
        }
    }
}
