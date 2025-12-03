/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : PowerSmash.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Heavy physical attack used by the Troll. Deals +80% bonus AP-based damage
*   and has a cooldown to prevent frequent usage.
*
* Responsibilities :
*   - Provide a strong physical attack option for the Troll
*   - Calculate raw damage using a 1.8x AP multiplier
*   - Apply a cooldown of 3 rounds after use
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;

namespace S1_D001_Monsterkampf_Simulator_ER.Skills.Troll
{
    internal class PowerSmash : SkillBase
    {
        private const float SkillMultiplier = 1.8f;
        private const int SkillCooldown = 3;

        /// <summary>
        /// Creates a new Power Smash skill instance.
        /// </summary>
        /// <param name="diagnostics">Diagnostics manager used for debug logging.</param>
        public PowerSmash(DiagnosticsManager diagnostics)
            : base(
            "Power Smash",
            "Unleashes a powerful smash, dealing +80% physical damage",
            SkillType.Aktive,
            DamageType.Physical,
            SkillMultiplier,
            diagnostics)
        {
            Cooldown = SkillCooldown;
        }

        /// <summary>
        /// Calculates raw damage using AP and the 1.8x multiplier.
        /// </summary>
        /// <param name="attacker">The monster performing the attack.</param>
        /// <returns>Raw damage value before reductions.</returns>
        public override float CalculateRawDamage(MonsterBase attacker)
        {
            float raw = attacker.Meta.AP * Power;
            _diagnostics.AddCheck($"{nameof(PowerSmash)}.{nameof(CalculateRawDamage)}: RawDamage = {raw}.");
            return raw;
        }
    }
}