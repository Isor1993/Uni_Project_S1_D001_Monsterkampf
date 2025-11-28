/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : ThrowStone.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Simple physical attack used by the Goblin. Deals +30% AP-based damage
*   and has a short cooldown to avoid constant spamming.
*
* Responsibilities :
*   - Provide a ranged physical attack option for the Goblin
*   - Calculate raw damage using a 1.3x AP multiplier
*   - Apply a cooldown of 1 round after use
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;

namespace S1_D001_Monsterkampf_Simulator_ER.Skills.Goblin
{
    internal class ThrowStone : SkillBase
    {
        private const float SkillMultiplier = 1.3f;
        private const int SkillCooldown = 1;

        /// <summary>
        /// Creates a new Throw Stone skill instance.
        /// </summary>
        /// <param name="diagnostics">Diagnostics manager used for debug logging.</param>
        public ThrowStone(DiagnosticsManager diagnostics)
            : base(
             "ThrowStone",
             "Throw a stone and deal 30% more damage.",
             SkillType.Aktive,
             DamageType.Physical,
             SkillMultiplier,
             diagnostics)
        {
            Cooldown = SkillCooldown;
        }

        /// <summary>
        /// Calculates raw damage using AP and the 1.3x multiplier.
        /// </summary>
        /// <param name="attacker">The monster performing the attack.</param>
        /// <returns>Raw damage value before reductions.</returns>
        public override float CalculateRawDamage(MonsterBase attacker)
        {
            float raw = attacker.Meta.AP * Power;
            _diagnostics.AddCheck($"{nameof(ThrowStone)}.{nameof(CalculateRawDamage)}: RawDamage = {raw}.");
            return raw;
        }
    }
}
