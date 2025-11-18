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

        protected StatusEffectBase(string name, int duration, DiagnosticsManager diagnosticsManager)
        {
            Name = name;
            Duration = duration;
            _diagnostics = diagnosticsManager;
        }
        public virtual void ApplyStartOfTurn(MonsterBase target)
        {
            // standardmäßig nichts
        }
        public virtual void ApplyEndOfTurn(MonsterBase target)
        {
            // standardmäßig nichts
        }
        // Jede Runde Wirkung
        public virtual void ApplyEffect(MonsterBase target)
        {
            //standardmäßig nichts
        }

        // Wird am Rundenende aufgerufen
        public virtual void Tick()
        {
            Duration--;

            _diagnostics.AddCheck($"{nameof(StatusEffectBase)}.{nameof(Tick)}: Successfully substracted duration {Duration} from {Name}.");
        }

        public virtual void OnExpire(MonsterBase target)
        {
            _diagnostics.AddCheck($"{nameof(StatusEffectBase)}.{nameof(OnExpire)}: {Name} expired on {target.Race}");
        }

        public virtual void OnTurnStart(MonsterBase target)
        {
            // Standardmäßig passiert nichts
        }
        public bool IsExpired => Duration <= 0;

       
        public virtual float ModifyFinalDamage(MonsterBase target, float damage)
        {
            //Later for something like debuff wich also inflict true damage
            return damage; // default: do nothing
        }
    }
}