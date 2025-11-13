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


namespace S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects
{
    internal abstract class StatusEffectBase
    {
        public string Name { get; }
        public int Duration { get; protected set; }

        protected StatusEffectBase(string name, int duration)
        {
            Name = name;
            Duration = duration;
        }

        // Jede Runde Wirkung
        public abstract void ApplyEffect(MonsterBase target);

        // Wird am Rundenende aufgerufen
        public virtual void Tick()
        {
            Duration--;
        }

        public bool IsExpired => Duration <= 0;
    }
}