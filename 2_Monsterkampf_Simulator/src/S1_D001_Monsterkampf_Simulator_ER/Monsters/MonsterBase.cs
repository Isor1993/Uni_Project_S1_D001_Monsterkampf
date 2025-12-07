/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : MonsterBase.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Abstract base class for all monsters. Holds core stats, resistances,
*   skills, status effects, and shared combat logic.
*
* Responsibilities :
*   - Provide shared monster data (meta, resistances, level, race)
*   - Handle attacking, taking damage and status effect processing
*   - Provide stat increase & level-up behavior
*   - Define abstract PrintSprite() for ASCII rendering
*   - Provide AI skill selection behavior
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Balancing;
using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Skills;
using S1_D001_Monsterkampf_Simulator_ER.Systems.Damage;
using S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects;
using System;

namespace S1_D001_Monsterkampf_Simulator_ER.Monsters
{
    /// <summary>
    /// The different monster race types.
    /// Used to identify the monster species (Orc, Troll, Goblin, Slime).
    public enum RaceType
    {
        None = 0,
        Orc = 1,
        Troll = 2,
        Goblin = 3,
        Slime = 4,
    }

    /// <summary>
    /// Represents all possible stats that can be increased through stat points.
    /// </summary>
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

        /// <summary>
        /// Monster flavor description shown in UI and log output.
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Contains active skills, passive skills, and support skills.
        /// </summary>
        public SkillPackage SkillPackage => _skills;

        /// <summary>
        /// The race classification of the monster.
        /// </summary>
        public RaceType Race { get; }

        /// <summary>
        /// The current level of the monster.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Creates a base monster and injects all core data required for combat.
        /// </summary>
        /// <param name="meta">Stat container (HP, AP, DP, Speed).</param>
        /// <param name="resistance">Resistance values for fire, water, poison, physical.</param>
        /// <param name="race">Assigned monster race.</param>
        /// <param name="level">Monster level.</param>
        /// <param name="skill">Skill package (active, passive, meta skills).</param>
        /// <param name="diagnosticsManager">Diagnostics manager for debug logging.</param>
        protected MonsterBase(MonsterMeta meta, MonsterResistance resistance, RaceType race, int level, SkillPackage skill, DiagnosticsManager diagnosticsManager)
        {
            Race = race;
            Level = level;
            _meta = meta;
            _resistance = resistance;
            _diagnostics = diagnosticsManager;
            _skills = skill;
        }

        /// <summary>
        /// Returns the monster's meta stats (HP, AP, DP, Speed).
        /// </summary>
        public MonsterMeta Meta => _meta;

        /// <summary>
        /// Returns all elemental/physical resistances.
        /// </summary>
        public MonsterResistance Resistance => _resistance;

        
        /// <summary>
        /// Performs an attack on another monster using a skill and
        /// a damage pipeline.
        /// </summary>
        /// <param name="target">The monster receiving the attack.</param>
        /// <param name="skill">The skill being used for the attack.</param>
        /// <param name="pipeline">Pipeline that processes damage steps.</param>
        /// <returns>Final damage dealt after all pipeline steps.</returns>
        /// <exception cref="ArgumentNullException">Thrown if target or skill is null.</exception>
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

        /// <summary>
        /// Creates a fallback basic attack skill used when no other skills are ready.
        /// </summary>
        /// <returns>A new BasicAttack instance.</returns>
        private SkillBase CreateBasicAttack()
        {
            return new BasicAttack(_diagnostics);
        }

        /// <summary>
        /// Decides which skill the AI should use this turn.
        /// Prioritizes high-power skills if off cooldown.
        /// Falls back to Basic Attack.
        /// </summary>
        /// <param name="random">Random number generator for tie-breaking.</param>
        /// <returns>The chosen SkillBase instance.</returns>
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

        /// <summary>
        /// Applies the passive skill if the monster has one.
        /// Called automatically during Spawn().
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        public void ClearStatusEffects()
        {
            _statusEffects.Clear();
            _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(ClearStatusEffects)}: Cleared all status effects for {Race}.");
        }

        /// <summary>
        /// Heals the monster by a specified amount
        /// without exceeding MaxHP.
        /// </summary>
        /// <param name="heal">Healing amount.</param>
        public virtual void Heal(float heal)
        {
            Meta.CurrentHP += heal;
           
            if (Meta.MaxHP < Meta.CurrentHP)
            {
                Meta.CurrentHP = Meta.MaxHP;
            }

            _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(Heal)}: {Race} got {heal} Hp healed.");
        }

        /// <summary>
        /// Adds a status effect (burn, poison, buff, debuff) to the monster.
        /// </summary>
        /// <param name="effect">The effect to apply.</param>
        public void AddStatusEffect(StatusEffectBase effect)
        {
            _statusEffects.Add(effect);
            _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(AddStatusEffect)}: Added status effect '{effect.Name}' on {Race}.");
        }

        /// <summary>
        /// Updates all status effect durations.
        /// Removes expired effects and triggers their OnExpire().
        /// Called once per turn.
        /// </summary>
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

        /// <summary>
        /// Applies all "start of turn" effects such as buffs, debuffs,
        /// or special triggers (e.g., regeneration).
        /// </summary>
        public void ProcessStartOfTurnEffects()
        {
            foreach (StatusEffectBase effect in _statusEffects)
            {
                effect.ApplyStartOfTurn(this);
            }

            _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(ProcessStartOfTurnEffects)}: Start-of-turn effects processed for {Race}.");
        }

        /// <summary>
        /// Applies all "end of turn" effects such as damage-over-time.
        /// </summary>
        public void ProcessEndOfTurnEffects()
        {
            foreach (StatusEffectBase effect in _statusEffects)
            {
                effect.ApplyEndOfTurn(this);   // DOT, Burn, Bleed, Poison etc.
            }

            _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(ProcessEndOfTurnEffects)}: End-of-turn effects processed for {Race}.");
        }

        /// <summary>
        /// Reduces the monster's HP by the incoming damage.
        /// Cannot reduce HP below zero.
        /// </summary>
        /// <param name="damage">Final damage to apply.</param>
        public virtual void TakeDamage(float damage)
        {
            _meta.CurrentHP -= damage;
            if (_meta.CurrentHP < 0)
            {
                _meta.CurrentHP = 0;
            }
            _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(TakeDamage)}: {Race} took {damage} damage.");
        }

        /// <summary>
        /// Retrieves all active status effects of a specific type.
        /// </summary>
        /// <typeparam name="T">The status effect subtype.</typeparam>
        /// <returns>All matching status effects.</returns>
        public IEnumerable<T> GetStatusEffects<T>() where T : StatusEffectBase
        {
            return _statusEffects.OfType<T>();
        }

        /// <summary>
        ///  Allows status effects to modify the final damage number.
        /// Ensures the result never drops below 1.
        /// </summary>
        /// <param name="damage">Incoming final damage.</param>
        /// <returns>Modified final damage.</returns>
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

        /// <summary>
        /// Updates cooldown timers for all skills.
        /// Should be called at the end of each turn.
        /// </summary>
        public void ProcessSkillCooldowns()
        {
            foreach (SkillBase skill in _skills.AllSkills)
            {
                skill.TickCooldown();
            }
            _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(ProcessSkillCooldowns)}: Cooldowns ticked for {Race}.");
        }

        /// <summary>
        /// Increases one specific stat using a stat point.
        /// The values come from MonsterBalancing.
        /// </summary>
        /// <param name="stat">Stat to increase.</param>
        /// <param name="balancing">Balancing data for stat gains.</param>
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
        

        /// <summary>
        /// Applies a level-up and scales all stats according to the balancing rules.
        /// </summary>
        /// <param name="balancing">Scaling values for HP, AP, DP, Speed.</param>
        public void ApplyLevelUp(MonsterBalancing balancing)
        {
            Level++;
            const int MultiplierBase = 1;
            Meta.MaxHP *= (balancing.HPScaling + MultiplierBase);
            Meta.CurrentHP = Meta.MaxHP;
            Meta.AP *= (balancing.APScaling + MultiplierBase);
            Meta.DP *= (balancing.DPScaling + MultiplierBase);
            Meta.Speed *= (balancing.SpeedScaling + MultiplierBase);

            Meta.DP = (int)Math.Round(Meta.DP);
            Meta.AP = (int)Math.Round(Meta.AP);
            Meta.Speed = (int)Math.Round(Meta.Speed);
            Meta.MaxHP = (int)Math.Round(Meta.MaxHP);
            Meta.CurrentHP = (int)Math.Round(Meta.CurrentHP);
            _diagnostics.AddCheck($"{nameof(MonsterBase)}.{nameof(ApplyLevelUp)}: LevelUp to {Level} and updated stats.");
        }

        /// <summary>
        /// Must be implemented by each monster to draw its ASCII sprite.
        /// </summary>
        public abstract void PrintSprite(bool isPlayer);

        /// <summary>
        /// Indicates whether the monster still has HP left.
        /// </summary>
        public bool IsAlive => _meta.CurrentHP > 0;
    }
}