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
    internal class PoisonEffect : StatusEffectBase
    {
        private readonly float _percent;

        public PoisonEffect(int duration, float damagePercent,DiagnosticsManager diagnostics)
            : base("Poison", duration,diagnostics)
        {
            _percent = damagePercent;
        }

        public override void ApplyEffect(MonsterBase target)
        {            
            float damage = target.Meta.MaxHP * _percent;
            target.TakeDamage(damage);
            _diagnostics.AddCheck($"{nameof(PoisonEffect)}.{nameof(ApplyEffect)}: {target.Race} took {damage} damage.");
        }
    }
}