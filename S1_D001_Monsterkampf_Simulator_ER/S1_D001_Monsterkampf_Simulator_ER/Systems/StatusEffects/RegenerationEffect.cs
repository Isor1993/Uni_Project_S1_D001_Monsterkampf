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
        private readonly float _multiplier;

        public RegenerationEffect(float multiplier, DiagnosticsManager diagnostics)
            : base("Regeneration", diagnostics)
        {
            _multiplier = multiplier;
        }

        public override void ApplyEffect(MonsterBase target)
        {
            float regeneration = target.Meta.MaxHP * _multiplier;
            regeneration = (Math.Max(1, regeneration));
            target.Heal(regeneration);
            _diagnostics.AddCheck($"{nameof(RegenerationEffect)}.{nameof(ApplyEffect)}: {target.Race} regenerated {regeneration} Hp ({_multiplier * 100:F0}%).");
        }
    }
}
