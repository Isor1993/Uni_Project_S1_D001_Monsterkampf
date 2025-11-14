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
    internal class RegenerationEffect : PermanentStatusEffect
    {
        private readonly float _percent;

        public RegenerationEffect(float percent, DiagnosticsManager diagnostics)
            : base("Regeneration", diagnostics)
        {
            _percent = percent;
        }

        public override void ApplyEffect(MonsterBase target)
        {
            float regeneration = target.Meta.MaxHP * _percent;
            target.Heal(regeneration);
            _diagnostics.AddCheck($"{nameof(RegenerationEffect)}.{nameof(ApplyEffect)}: {target.Race} regenerated {regeneration} Hp.");
        }
    }
}
