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


namespace S1_D001_Monsterkampf_Simulator_ER.Monsters
{

    // === Dependencies ===

    // === Fields ===

    public enum RaceType
    {
        None = 0,
        Orc = 1,
        Troll = 2,
        Goblin = 3,
        Slime = 4,

    }


    internal abstract class MonsterBase
    {
        protected MonsterMeta _meta;
        protected MonsterResistance _resistance;


        public RaceType Race { get;  }


        public int Level { get; }


        protected MonsterBase(MonsterMeta meta, MonsterResistance resistance, RaceType race, int level)
        {
            Race = race;
            Level = level;
            _meta = meta;
            _resistance = resistance;
        }


        public MonsterMeta Meta => _meta;


        public MonsterResistance Resistance => _resistance;


        public abstract void Spawn();


        public abstract void Attack(MonsterBase target);


        public virtual void UsePasiveSkill() { }


        public virtual void UseAktiveSkill() { }


        public virtual void Heal() { }


        public virtual void TakeDamage(float damage)
        {
            _meta.HP -= damage;
            if (_meta.HP < 0)
            {
                _meta.HP = 0;
            }
        }


        public bool IsAlive => _meta.HP > 0;
    }
}
