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
using S1_D001_Monsterkampf_Simulator_ER.Player;

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


        public virtual float CalculateRawDamage(MonsterBase attacker)
        {
            float raw = attacker.Meta.AP * Power;
            _diagnostics.AddCheck($"{nameof(SkillBase)}.{nameof(CalculateRawDamage)}: RawDamage = {raw} (AP={attacker.Meta.AP}, Power={Power}).");
            return raw;
        }

        /// <summary>
        /// Wird VOR dem Schaden ausgeführt (Buffs, Self-Effekte, Setup).
        /// </summary>
        public virtual void OnCast(MonsterBase caster)
        {
            // Default: keine Effekte
        }

        /// <summary>
        /// Wird NACH dem Schaden ausgeführt (DoTs, Debuffs, StatusEffects).
        /// </summary>
        public virtual void OnHit(MonsterBase attacker, MonsterBase target)
        {
            // Default: keine Effekte
        }
        public void StartCooldown()
        {
            if (Cooldown > 0)
            {
                CurrentCooldown = Cooldown;
                _diagnostics.AddCheck($"{nameof(SkillBase)}.{nameof(StartCooldown)}: {Name} cooldown → {CurrentCooldown}");
            }
        }
        public void TickCooldown()
        {
            if (CurrentCooldown > 0)
            {
                CurrentCooldown--;
                _diagnostics.AddCheck($"{nameof(SkillBase)}.{nameof(TickCooldown)}: {Name} cooldown → {CurrentCooldown}");
            }
        }
        public virtual void OnVictory(MonsterBase owner, PlayerData playerData, DiagnosticsManager diagnostics)
        {
            // Default: nichts tun
        }
    }
}
