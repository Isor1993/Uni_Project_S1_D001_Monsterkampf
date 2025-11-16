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
using S1_D001_Monsterkampf_Simulator_ER.Systems.Damage;
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


        protected MonsterBase(MonsterMeta meta, MonsterResistance resistance, RaceType race, int level,SkillPackage skill,DiagnosticsManager diagnosticsManager)
        {
            Race = race;
            Level = level;
            _meta = meta;
            _resistance = resistance;
            _diagnostics = diagnosticsManager;
            _skills=skill;
        }

       

        public MonsterMeta Meta => _meta;


        public MonsterResistance Resistance => _resistance;


        public abstract void Spawn();


        public virtual float Attack(MonsterBase target,SkillBase skill,DamagePipeline pipeline)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            if (skill == null)
            {
                throw new ArgumentNullException(nameof(skill));
            }
            float damage = pipeline.Execute(this, target, skill);

            skill.StartCooldown();

            //TODO Cooldown erst später implementieren
            //TODO skill.TriggerCooldown() kommt später

            return damage;
        }
        private SkillBase CreateBasicAttack()
        {
            return new BasicAttack(_diagnostics);
        }
        public SkillBase ChooseSkillForAI(RandomManager random)
        {
            // 1. Skills sammeln, die bereit sind (IsReady == true)
            List<SkillBase> readySkills = Skills.ActiveSkills
                .Where(skill => skill.IsReady)
                .ToList();

            // 2. Basic Attack als Fallback erzeugen
            SkillBase basicAttack = CreateBasicAttack();

            // 3. Falls KEIN Skill verfügbar → Basic Attack benutzen
            if (readySkills.Count == 0)
            {
                _diagnostics.AddCheck($"{Race} AI uses BASIC ATTACK (no skills ready).");
                return basicAttack;
            }

            // 4. Skills nach Stärke sortieren (stärkster Skill zuerst)
            readySkills = readySkills
                .OrderByDescending(skill => skill.Power)
                .ToList();

            // 5. Vorschaden für Basic vs Skill berechnen (ohne Pipeline, nur Preview)
            float basicDamagePreview = Meta.AP * 1.0f; // BasicAttack.Power = 1.0f
            SkillBase strongestSkill = readySkills[0];
            float skillDamagePreview = Meta.AP * strongestSkill.Power;

            // 6. Logische Entscheidung:
            // Wenn der Skill deutlich stärker ist → Skill verwenden
            if (skillDamagePreview >= basicDamagePreview * 1.1f)
            {
                _diagnostics.AddCheck($"{Race} AI chooses strongest SKILL '{strongestSkill.Name}'.");
                return strongestSkill;
            }
           
            // 7. Beide Angriffe sind ähnlich stark → Random entscheiden
            int choice = random.Next(2);
            if (choice == 0)
            {
                _diagnostics.AddCheck($"{Race} AI randomly chooses SKILL '{strongestSkill.Name}'.");
                return strongestSkill;
            }

            _diagnostics.AddCheck($"{Race} AI randomly chooses BASIC ATTACK over skill.");
            return basicAttack;
        }


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
            _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(AddStatusEffect)}: Added status effect '{effect.Name}' on {Race}.");
        }

        public void ProcessStatusEffects()
        {            
            List<StatusEffectBase> effects = new List<StatusEffectBase>(_statusEffects);

            foreach (StatusEffectBase effect in effects)
            {
            
                effect.ApplyEffect(this);
             
                effect.Tick();
          
                if (effect.IsExpired)
                {
                    effect.OnExpire(this);
                    _statusEffects.Remove(effect);

                    _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(ProcessStatusEffects)}: '{effect.Name}' expired on {Race}.");
                }
            }
                _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(ProcessStatusEffects)}: Processed {effects.Count} status effects for {Race}.");
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
        //TODO
        public IEnumerable<T> GetStatusEffects<T>() where T : StatusEffectBase
        {
            return _statusEffects.OfType<T>();
        }

        //TODO
        public virtual float ModifyFinalDamage(float damage)
        {
            return damage; // wird später durch Absorb/Shield/Thorns modifiziert
        }


        public bool IsAlive => _meta.CurrentHP > 0;
    }
}
