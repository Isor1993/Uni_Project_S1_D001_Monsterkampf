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


using S1_D001_Monsterkampf_Simulator_ER.Monsters;

namespace S1_D001_Monsterkampf_Simulator_ER.Skills
{

    public enum SkillType
    {
        None = 0,
        Aktive= 1,
        Passive= 2,
    }


    public enum DamageType
    {
        Unknown = 0,
        Physical= 1,
        Fire= 2,
        Water= 3,
    }


    internal class SkillBase
    {
        // === Dependencies ===

        // === Fields ===

        public string Name { get; }


        public string Description { get; }


        public SkillType Type { get; }


        public DamageType DamageType { get; }


        public float Power { get; }

        public SkillBase(string name, string description, SkillType type,DamageType damageType,float power)
        {
            Name = name;
            Description = description;
            Type = type;
            DamageType = damageType;
            Power = power;
        }

        public virtual float CalculateDamage(MonsterBase attacker,MonsterBase target)
        {
            return attacker.Meta.AP * Power;
        }


        public virtual void Apply (MonsterBase user, MonsterBase target) { }
    }
}
