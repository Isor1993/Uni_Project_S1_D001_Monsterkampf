/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : 
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
* *
* History :
* xx.xx.2025 ER Created
******************************************************************************/


using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;

namespace S1_D001_Monsterkampf_Simulator_ER.Skills
{

    public enum SkillType
    {
        None = 0,
        Aktive = 1,
        Passive = 2,
        Meta = 3,
    }


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

        public string Name { get; }


        public string Description { get; }


        public SkillType Type { get; }


        public DamageType DamageType { get; }


        public float Power { get; }

        // === Cooldown-System ===
        public int Cooldown { get; protected set; } = 0;       // how many rounds before reuse
        public int CurrentCooldown { get; set; } = 0;          // rounds left until ready
        public bool IsReady => CurrentCooldown == 0;

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
        /// 1. Base Damege : rawDamage= AP*Power
        /// 2. Reduktion DP :afterDefense= rawDamage-DP
        /// 3. Reduktion Resistance: finalDamage=afterDefense*(1-Resistance)
        /// 4. Minimal 1Damage: finalDamage = Math.Max(1,finalDamage)
        /// 5. StatusEffects : Like Absorb
        /// 6. AfterEffects : Like´Heal/Shields/thorns... .
        /// 
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public virtual float CalculateDamage(MonsterBase attacker, MonsterBase target)
        {
            float damage = attacker.Meta.AP * Power;
            _diagnostics.AddCheck($"{nameof(SkillBase)}.{nameof(CalculateDamage)}: Calculated damage = {damage}.");
            return damage;
        }


        public virtual void Apply(MonsterBase user, MonsterBase target) { }
    }
}
