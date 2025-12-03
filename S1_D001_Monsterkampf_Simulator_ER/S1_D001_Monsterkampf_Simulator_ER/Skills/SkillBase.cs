/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : SkillBase.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Base class for all monster skills. Defines shared properties such as name,
*   description, damage type, power and cooldown behavior. Also provides hooks
*   for casting and hit effects.
*
* Responsibilities :
*   - Provide shared data for all skill types
*   - Calculate raw damage based on AP and power
*   - Handle cooldown logic
*   - Offer OnCast and OnHit hooks for derived skills
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;

namespace S1_D001_Monsterkampf_Simulator_ER.Skills
{
    /// <summary>
    /// Defines the functional category of a skill.
    /// </summary>
    public enum SkillType
    {
        None = 0,
        Aktive = 1,
        Passive = 2,
        Meta = 3,
    }

    /// <summary>
    /// Defines the elemental/physical damage type of a skill.
    /// </summary>
    public enum DamageType
    {
        None = 0,
        Physical = 1,
        Fire = 2,
        Water = 3,
        Poison = 4,
    }

    internal class SkillBase
    {
        // === Dependencies ===
        protected readonly DiagnosticsManager _diagnostics;

        // === Fields ===

        /// <summary>
        /// Name of the skill.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Short description of the skill’s behavior.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Defines whether the skill is active, passive or meta.
        /// </summary>
        public SkillType Type { get; }

        /// <summary>
        /// Damage type used for resistance calculation.
        /// </summary>
        public DamageType DamageType { get; }

        /// <summary>
        /// Damage multiplier used during raw damage calculation.
        /// </summary>
        public float Power { get; }

        // === Cooldown-System ===

        /// <summary>
        /// Base cooldown duration in rounds.
        /// </summary>
        public int Cooldown { get; protected set; } = 0;

        /// <summary>
        /// Remaining cooldown until the skill becomes ready.
        /// </summary>
        public int CurrentCooldown { get; set; } = 0;

        /// <summary>
        /// Indicates whether the skill can currently be used.
        /// </summary>
        public bool IsReady => CurrentCooldown == 0;

        /// <summary>
        /// Creates a new skill definition.
        /// </summary>
        /// <param name="name">Name of the skill.</param>
        /// <param name="description">Description of the skill.</param>
        /// <param name="type">Category of the skill (active/passive/meta).</param>
        /// <param name="damageType">Damage type used for resistance calculation.</param>
        /// <param name="power">Damage multiplier when dealing damage.</param>
        /// <param name="diagnosticsManager">Diagnostics manager used for debug logging.</param>
        public SkillBase(string name, string description, SkillType type, DamageType damageType, float power, DiagnosticsManager diagnosticsManager)
        {
            Name = name;
            Description = description;
            Type = type;
            DamageType = damageType;
            Power = power;
            _diagnostics = diagnosticsManager;
        }

        /// <summary>
        /// Calculates raw skill damage using the attacker’s AP and the skill’s power.
        /// </summary>
        /// <param name="attacker">The monster performing the attack.</param>
        /// <returns>Raw damage before reductions.</returns>
        public virtual float CalculateRawDamage(MonsterBase attacker)
        {
            float raw = attacker.Meta.AP * Power;
            _diagnostics.AddCheck($"{nameof(SkillBase)}.{nameof(CalculateRawDamage)}: RawDamage = {raw} (AP={attacker.Meta.AP}, Power={Power}).");
            return raw;
        }

        /// <summary>
        /// Hook called when the skill is activated/cast.
        /// Default behavior: no action.
        /// </summary>
        /// <param name="caster">The monster casting the skill.</param>
        public virtual void OnCast(MonsterBase caster)
        {
            // Default: keine Effekte
        }

        /// <summary>
        /// Hook called after damage is applied to the target.
        /// Default behavior: no action.
        /// </summary>
        /// <param name="attacker">The monster performing the attack.</param>
        /// <param name="target">The monster receiving the hit.</param>
        public virtual void OnHit(MonsterBase attacker, MonsterBase target)
        {
        }

        /// <summary>
        /// Starts the cooldown timer and adds an extra 1-round offset
        /// to prevent immediate reuse on the same turn.
        /// </summary>
        public void StartCooldown()
        {
            if (Cooldown > 0)
            {
                const int Offset = 1;
                CurrentCooldown = Cooldown + Offset;
                _diagnostics.AddCheck($"{nameof(SkillBase)}.{nameof(StartCooldown)}: {Name} cooldown → {CurrentCooldown}");
            }
        }

        /// <summary>
        /// Reduces the cooldown timer by one round.
        /// </summary>
        public void TickCooldown()
        {
            if (CurrentCooldown > 0)
            {
                CurrentCooldown--;
                _diagnostics.AddCheck($"{nameof(SkillBase)}.{nameof(TickCooldown)}: {Name} cooldown → {CurrentCooldown}");
            }
        }

        /// <summary>
        /// Allows skills to modify the victory reward (used by some passive skills).
        /// </summary>
        /// <param name="reward">Base reward value.</param>
        /// <returns>Modified reward value.</returns>
        public virtual float ModifyVictoryReward(float reward)
        {
            return reward;
        }
    }
}