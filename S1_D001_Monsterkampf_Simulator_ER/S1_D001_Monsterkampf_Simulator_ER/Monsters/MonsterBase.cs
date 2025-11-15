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
using S1_D001_Monsterkampf_Simulator_ER.Skills;
using S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects;
using System.Net.WebSockets;

namespace S1_D001_Monsterkampf_Simulator_ER.Monsters
{


    public enum RaceType
    {
        None = 0,
        Orc = 1,
        Troll = 2,
        Goblin = 3,
        Slime = 4,

    }
    public enum EfectsType
    {

    }


    internal abstract class MonsterBase
    {
        // === Dependencies ===
        protected readonly DiagnosticsManager _diagnostics;
        // === Fields ===
        protected MonsterMeta _meta;
        protected MonsterResistance _resistance;
        protected SkillPackage _skills;
        private List<StatusEffectBase> _statusEffects = new List<StatusEffectBase>();


        public SkillPackage Skills => _skills;

        public RaceType Race { get; }


        public int Level { get; }


        protected MonsterBase(MonsterMeta meta, MonsterResistance resistance, RaceType race, int level,SkillPackage skills,DiagnosticsManager diagnosticsManager)
        {
            Race = race;
            Level = level;
            _meta = meta;
            _resistance = resistance;
            _diagnostics = diagnosticsManager;
            _skills=skills;
        }


        public MonsterMeta Meta => _meta;


        public MonsterResistance Resistance => _resistance;


        public abstract void Spawn();


        public abstract void Attack(MonsterBase target);


        public virtual void UsePasiveSkill() { }


        public virtual void UseAktiveSkill() { }


        public virtual void Heal(float heal)
        {
            Meta.CurrentHP += heal;
            if ( Meta.MaxHP< Meta.CurrentHP)
            {
                Meta.CurrentHP = Meta.MaxHP;
            }

            _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(Heal)}: {Race} got {heal} Hp healed.");

        }

        public void AddStatusEffect(StatusEffectBase effect)
        {
            _statusEffects.Add(effect);
            _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(AddStatusEffect)}: Added {effect} on {Race}");
        }

        public void ProcessStatusEffects()
        {
            foreach (StatusEffectBase effect in _statusEffects)
            {
                effect.ApplyEffect(this);
                effect.Tick();
            }
            List<StatusEffectBase> toRemove = new List<StatusEffectBase>();

            foreach (StatusEffectBase effect in _statusEffects)
            {
                if (effect.IsExpired)
                {                    
                    toRemove.Add(effect);
                }
            }
            foreach(StatusEffectBase expired in toRemove)
            {
                expired.OnExpire(this);
            _statusEffects.Remove(expired);
            }
            _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(ProcessStatusEffects)}: Processed all status effects successfully.");
        }


        public virtual void TakeDamage(float damage)
        {
            _meta.CurrentHP -= damage;
            if (_meta.CurrentHP < 0)
            {
                _meta.CurrentHP = 0;
            }
            _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(TakeDamage)}: {Race} took {damage} damage.");
        }


        public bool IsAlive => _meta.CurrentHP > 0;
    }
}
