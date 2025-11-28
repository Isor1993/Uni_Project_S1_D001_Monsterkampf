/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : Waterball.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Water-type skill used by the Slime. Deals +50% AP-based damage and has
*   a cooldown to prevent frequent usage.
*
* Responsibilities :
*   - Provide a water-element attack for the Slime
*   - Calculate raw damage using a 1.5x AP multiplier
*   - Apply a cooldown of 2 rounds after use
*
* History :
*   03.12.2025 ER Created
******************************************************************************/
using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;



namespace S1_D001_Monsterkampf_Simulator_ER.Skills.Slime
{
    internal class Waterball : SkillBase
    {
        // === Fields ===
        private const float SkillMultiplier = 1.5f;
        private const int SkillCooldown = 2;

        /// <summary>
        /// Creates a new Waterball skill instance.
        /// </summary>
        /// <param name="diagnostics">Diagnostics manager used for debug logging.</param>
        public Waterball(DiagnosticsManager diagnostics)
            : base(
                 "Waterball",
                 "Shoots a waterball which deals 50% more damage.",
                 SkillType.Aktive,
                 DamageType.Water,
                 SkillMultiplier,
                 diagnostics)
        {
            Cooldown = SkillCooldown;
        }

        /// <summary>
        /// Calculates raw damage using AP and the 1.5x multiplier.
        /// </summary>
        /// <param name="attacker">The monster performing the attack.</param>
        /// <returns>Raw damage value before reductions.</returns>
        public override float CalculateRawDamage(MonsterBase attacker)
        {
            float raw = attacker.Meta.AP * Power;
            _diagnostics.AddCheck($"{nameof(Waterball)}.{nameof(CalculateRawDamage)}: RawDamage = {raw}.");
            return raw;
        }
    }
}
