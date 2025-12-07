/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : PassiveSkill_Regeneration.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Passive Troll skill that applies a regeneration status effect which restores
*   a percentage of the monster's maximum HP each round.
*
* Responsibilities :
*   - Apply a regeneration effect to the Troll at battle start
*   - Regenerate MaxHP based on a multiplier each round
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects;

namespace S1_D001_Monsterkampf_Simulator_ER.Skills.Troll
{
    internal class PassiveSkill_Regeneration : SkillBase, IPassiveSkill
    {
        // === Fields ===

        private const float SkillMultiplier = 0.01f;
        private const int SkillCooldown = 0;

        /// <summary>
        /// Creates a new passive regeneration skill instance.
        /// </summary>
        /// <param name="diagnostics">Diagnostics manager used for debug logging.</param>
        public PassiveSkill_Regeneration(DiagnosticsManager diagnostics)
            : base(
                 "Regeneration",
                 "Regenerates % of Max HP each round",
                 SkillType.Passive,
                 DamageType.None,
                 0f,
                 diagnostics)
        {
            Cooldown = SkillCooldown;
        }

        /// <summary>
        /// Applies the regeneration passive to the owning monster.
        /// </summary>
        /// <param name="user">The monster receiving the passive regeneration effect.</param>
        public void ApplyPassive(MonsterBase user)
        {
            user.AddStatusEffect(new RegenerationEffect(SkillMultiplier, _diagnostics));
            _diagnostics.AddCheck($"{nameof(PassiveSkill_Regeneration)}.{nameof(ApplyPassive)}: Applied regeneration effect (% Health) on {user.Race}.");
        }
    }
}