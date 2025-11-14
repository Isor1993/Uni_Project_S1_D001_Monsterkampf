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
using System.Diagnostics.Tracing;

namespace S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects
{
    internal class TribeScream:StatusEffectBase
    {
        private readonly float _percent;

        public TribeScream(DiagnosticsManager diagnostics)
            :base(
                 "Tribe Scream",
                 5,
                 diagnostics)
        {            
        }

        public override void ApplyEffect(MonsterBase target)
        {
            
        }
    }
}
