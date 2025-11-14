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

namespace S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects
{
    internal abstract class StatusEffectBase
    {

        protected readonly DiagnosticsManager _diagnostics;
        public string Name { get; }
        public int Duration { get; protected set; }

        protected StatusEffectBase(string name, int duration,DiagnosticsManager diagnosticsManager)
        {
            Name = name;
            Duration = duration;
            _diagnostics = diagnosticsManager;
        }

        // Jede Runde Wirkung
        public abstract void ApplyEffect(MonsterBase target);

        // Wird am Rundenende aufgerufen
        public virtual void Tick()
        {
            Duration--;

            _diagnostics.AddCheck($"{nameof(StatusEffectBase)}.{nameof(Tick)}: Successfully substracted duration {Duration}.");
        }

        public bool IsExpired => Duration <= 0;
    }
}