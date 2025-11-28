/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : PassiveSkill_Absorb.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Passive skill used by the Slime. Grants a permanent absorb effect that
*   reduces incoming damage by 30%, ensuring higher survivability.
*
* Responsibilities :
*   - Apply a permanent AbsorbEffect to the monster at battle start
*   - Provide the Slime with increased defensive capabilities
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects;

namespace S1_D001_Monsterkampf_Simulator_ER.Skills.Slime
{
    internal class PassiveSkill_Absorb:SkillBase,IPassiveSkill
    {        
        // === Fields ===
        private const float SkillMultiplier = 0.3f;        
        private const int SkillCooldown = 0;

        /// <summary>
        /// Creates a new Absorb passive skill instance.
        /// </summary>
        /// <param name="diagnostics">Diagnostics manager used for debug logging.</param>
        public PassiveSkill_Absorb(DiagnosticsManager diagnostics)
            : base(
                 "Absorb",
                 "Absorbs 30% of all incoming damage.",
                 SkillType.Passive,
                 DamageType.None,
                 0f,
                 diagnostics)
        {
            Cooldown = SkillCooldown;
        }

        /// <summary>
        /// Applies the absorb passive effect to the monster at battle start.
        /// </summary>
        /// <param name="user">The monster receiving the passive absorb effect.</param>
        public void ApplyPassive(MonsterBase user)
        {
            user.AddStatusEffect(new AbsorbEffect(SkillMultiplier,_diagnostics));
            _diagnostics.AddCheck($"{nameof(PassiveSkill_Absorb)}.{nameof(ApplyPassive)}: Applied absorb effect on {user.Race}.");
        }
    }    
}
