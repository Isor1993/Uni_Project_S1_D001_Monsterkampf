/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : BasicAttack.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Represents a simple, standard AP-based attack with no cooldown.
*   This is the default attack available to all monsters.
*
* Responsibilities :
*   - Provide a basic physical attack without special effects
*   - Calculate raw damage based on AP and a 1.0x multiplier
*   - Always remain available (no cooldown)
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;


namespace S1_D001_Monsterkampf_Simulator_ER.Skills
{
    internal class BasicAttack : SkillBase
    {
        private const float SkillMultiplier = 1.0f;
        private const int SkillDuration = 0;
        private const int SkillCooldown = 0;

        /// <summary>
        /// Creates a new basic attack instance.
        /// </summary>
        /// <param name="diagnostics">Diagnostics manager used for debug logging.</param>
        public BasicAttack(DiagnosticsManager diagnostics)
            : base(
                  "Basic Attack",
                  "A standard physical attack based on AP.",
                  SkillType.Aktive,
                  DamageType.Physical,
                  SkillMultiplier, 
                  diagnostics
                  )
        {
            Cooldown = SkillCooldown;
            CurrentCooldown = SkillDuration;
        }
        /// <summary>
        /// Calculates raw damage using AP and the fixed multiplier.
        /// </summary>
        /// <param name="attacker">The monster performing the attack.</param>
        /// <returns>Raw damage value before reductions.</returns>
        public override float CalculateRawDamage(MonsterBase attacker)
        {
            float raw = attacker.Meta.AP * Power;
            _diagnostics.AddCheck($"{nameof(BasicAttack)}.{nameof(CalculateRawDamage)}: RawDamage = {raw}.");
            return raw;
        }
    }
}