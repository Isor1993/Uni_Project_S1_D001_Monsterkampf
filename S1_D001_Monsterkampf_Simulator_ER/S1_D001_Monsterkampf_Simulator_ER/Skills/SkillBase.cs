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
    /// 
    /// </summary>
    public enum SkillType
    {
        None = 0,
        Aktive = 1,
        Passive = 2,
        Meta = 3,
    }

    /// <summary>
    /// 
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
        /// 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// 
        /// </summary>
        public SkillType Type { get; }

        /// <summary>
        /// 
        /// </summary>
        public DamageType DamageType { get; }

        /// <summary>
        /// 
        /// </summary>
        public float Power { get; }

        // === Cooldown-System ===

        /// <summary>
        /// 
        /// </summary>
        public int Cooldown { get; protected set; } = 0;       // how many rounds before reuse

        /// <summary>
        /// 
        /// </summary>
        public int CurrentCooldown { get; set; } = 0;          // rounds left until ready

        /// <summary>
        /// 
        /// </summary>
        public bool IsReady => CurrentCooldown == 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="type"></param>
        /// <param name="damageType"></param>
        /// <param name="power"></param>
        /// <param name="diagnosticsManager"></param>
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
        /// 
        /// </summary>
        /// <param name="attacker"></param>
        /// <returns></returns>
        public virtual float CalculateRawDamage(MonsterBase attacker)
        {
            float raw = attacker.Meta.AP * Power;
            _diagnostics.AddCheck($"{nameof(SkillBase)}.{nameof(CalculateRawDamage)}: RawDamage = {raw} (AP={attacker.Meta.AP}, Power={Power}).");
            return raw;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="caster"></param>
        public virtual void OnCast(MonsterBase caster)
        {
            // Default: keine Effekte
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="target"></param>
        public virtual void OnHit(MonsterBase attacker, MonsterBase target)
        {

        }

        /// <summary>
        /// 
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
        /// 
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
        /// 
        /// </summary>
        /// <param name="reward"></param>
        /// <returns></returns>
        public virtual float ModifyVictoryReward(float reward)
        {
            return reward;
        }
    }
}
