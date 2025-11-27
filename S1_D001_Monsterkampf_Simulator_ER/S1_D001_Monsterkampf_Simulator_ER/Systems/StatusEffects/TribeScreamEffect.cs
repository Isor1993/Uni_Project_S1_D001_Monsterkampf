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
    internal class TribeScreamEffect : StatusEffectBase
    {
        private readonly float _multiplier;
        private float _baseAp;
        private bool _applied = false;

        public TribeScreamEffect(float multiplier, int duration, DiagnosticsManager diagnostics)
            : base("Tribe Scream", duration, diagnostics)
        {
            _multiplier = multiplier;
        }

        public override void ApplyStartOfTurn(MonsterBase target)
        {
            if (_applied)
            {
                return;
            }
            _applied = true;
            _baseAp = target.Meta.AP;
            target.Meta.AP = _baseAp * _multiplier;
            _diagnostics.AddCheck($"{nameof(TribeScreamEffect)}.{nameof(ApplyStartOfTurn)}: Increased AP from {_baseAp} to {target.Meta.AP}.");
        }

        public override void OnExpire(MonsterBase target)
        {
            target.Meta.AP = _baseAp;
            _diagnostics.AddCheck($"{nameof(TribeScreamEffect)}.{nameof(OnExpire)}: Restored AP back to {_baseAp}.");
        }
    }
}