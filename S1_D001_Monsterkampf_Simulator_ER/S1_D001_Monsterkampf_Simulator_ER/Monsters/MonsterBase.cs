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


using S1_D001_Monsterkampf_Simulator_ER.Balancing;
using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Player;
using S1_D001_Monsterkampf_Simulator_ER.Skills;
using S1_D001_Monsterkampf_Simulator_ER.Skills.Goblin;
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
    //TODO alle enums viel in eine cs datei ??
    public enum StatType
    {
        MaxHP,
        AP,
        DP,
        Speed

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


        public abstract string Description { get; }

        public SkillPackage SkillPackage => _skills;

        public RaceType Race { get; }


        public int Level { get; set; }


        protected MonsterBase(MonsterMeta meta, MonsterResistance resistance, RaceType race, int level, SkillPackage skill, DiagnosticsManager diagnosticsManager)
        {
            Race = race;
            Level = level;
            _meta = meta;
            _resistance = resistance;
            _diagnostics = diagnosticsManager;
            _skills = skill;
        }



        public MonsterMeta Meta => _meta;


        public MonsterResistance Resistance => _resistance;


        public abstract void Spawn();

       
        public virtual float Attack(MonsterBase target, SkillBase skill, DamagePipeline pipeline)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            if (skill == null)
            {
                throw new ArgumentNullException(nameof(skill));
            }
            float finalDamage = pipeline.Execute(this, target, skill);

            skill.StartCooldown();

            return finalDamage;
        }
        private SkillBase CreateBasicAttack()
        {
            return new BasicAttack(_diagnostics);
        }
        public SkillBase ChooseSkillForAI(RandomManager random)
        {
            // 1. Skills sammeln, die bereit sind (IsReady == true)
            List<SkillBase> readySkills = SkillPackage.ActiveSkills
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


        public virtual void UsePasiveSkill()
        {
            if (_skills.PassiveSkill is IPassiveSkill passive)
            {
                passive.ApplyPassive(this);
            }
            else
            {
                _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(UsePasiveSkill)}: No spawn passive to apply for {Race}.");
            }
        }


        public virtual void UseAktiveSkill() { }


        public virtual void Heal(float heal)
        {
            Meta.CurrentHP += heal;
            if (Meta.MaxHP < Meta.CurrentHP)
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

        public void ProcessStatusEffectDurations()
        {
            List<StatusEffectBase> expired = new();

            foreach (StatusEffectBase effect in _statusEffects)
            {
                effect.Tick();

                if (effect.IsExpired)
                {
                    expired.Add(effect);
                }
            }

            foreach (StatusEffectBase effect in expired)
            {
                effect.OnExpire(this);
                _statusEffects.Remove(effect);
            }

            _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(ProcessStatusEffectDurations)}: Duration tick processed for {Race}.");
        }

        public void ProcessStartOfTurnEffects()
        {
            foreach (StatusEffectBase effect in _statusEffects)
            {
                effect.ApplyStartOfTurn(this);
            }

            _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(ProcessStartOfTurnEffects)}: Start-of-turn effects processed for {Race}.");
        }
        public void ProcessEndOfTurnEffects()
        {
            foreach (StatusEffectBase effect in _statusEffects)
            {
                effect.ApplyEndOfTurn(this);   // DOT, Burn, Bleed, Poison etc.
            }

            _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(ProcessEndOfTurnEffects)}: End-of-turn effects processed for {Race}.");
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

        public IEnumerable<T> GetStatusEffects<T>() where T : StatusEffectBase
        {
            return _statusEffects.OfType<T>();
        }

        /// <summary>
        /// für später efecte die finaldamage manupulieren
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public virtual float ModifyFinalDamage(float damage)
        {
            float modified = damage;
            foreach (StatusEffectBase effect in _statusEffects)
            {
                modified = effect.ModifyFinalDamage(this, modified);
            }

            modified = Math.Max(1, modified);
            return modified;
        }
        public void ProcessSkillCooldowns()
        {
            foreach (SkillBase skill in _skills.AllSkills)
            {
                skill.TickCooldown();
            }
            _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(ProcessSkillCooldowns)}: Cooldowns ticked for {Race}.");
        }

        public void ApplyStatPointIncrease(StatType stat, MonsterBalancing balancing)
        {

            switch (stat)
            {
                case StatType.MaxHP:
                    float oldMaxHP = Meta.MaxHP;
                    Meta.MaxHP += balancing.StatIncrease_HP;
                    Meta.CurrentHP = Meta.MaxHP;
                    _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(ApplyStatPointIncrease)}: Increased max HP {oldMaxHP} to {Meta.MaxHP}.");
                    break;
                case StatType.AP:
                    float oldAP = Meta.AP;
                    Meta.AP += balancing.StatIncrease_AP;
                    _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(ApplyStatPointIncrease)}: Increased max AP {oldAP} to {Meta.AP}.");
                    break;
                case StatType.DP:
                    float oldDP = Meta.DP;
                    Meta.DP += balancing.StatIncrease_DP;
                    _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(ApplyStatPointIncrease)}: Increased max DP {oldDP} to {Meta.DP}.");
                    break;
                case StatType.Speed:
                    float oldMaxSpeed = Meta.Speed;
                    Meta.Speed += balancing.StatIncrease_Speed;
                    _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(ApplyStatPointIncrease)}: Increased max Speed {oldMaxSpeed} to {Meta.Speed}.");
                    break;
                default:
                    _diagnostics.AddError($"{nameof(MonsterBase)}.{nameof(ApplyStatPointIncrease)}: No StatType found.");
                    break;
            }

        }
        public void ApplyLevelUp(MonsterMeta newMeta,MonsterBalancing balancing)
        {
            Level++;
            const int MultiplierBase = 1;
            Meta.MaxHP *= (balancing.HPScaling+ MultiplierBase);
            Meta.CurrentHP =Meta.MaxHP;
            Meta.AP *= (balancing.APScaling+ MultiplierBase);
            Meta.DP *= (balancing.DPScaling+ MultiplierBase);
            Meta.Speed *= (balancing.SpeedScaling+ MultiplierBase);
            _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(ApplyLevelUp)}: LevelUp to {Level} and updated stats.");
        }

        public bool IsAlive => _meta.CurrentHP > 0;
    }
}
