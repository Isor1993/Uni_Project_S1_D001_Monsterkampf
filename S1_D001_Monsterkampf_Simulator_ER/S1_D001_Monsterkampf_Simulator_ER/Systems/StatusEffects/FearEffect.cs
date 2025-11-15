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
    internal class FearEffect:PermanentStatusEffect
    {
        // === Fields ===
        private readonly float _percent;
        private float _baseSpeed;
        private bool _applied = false;

        public FearEffect(float percent, DiagnosticsManager diagnostics)
           : base(
                "Fear",
                diagnostics)
        {
            _percent = percent;
        }

        public override void ApplyEffect(MonsterBase target)
        {
            if (_applied)
            {
                return;
            }
            _applied=true;
            
            _baseSpeed = target.Meta.Speed;
            target.Meta.Speed= Math.Max(1,target.Meta.Speed * _percent);

            _diagnostics.AddCheck($"{nameof(FearEffect)}.{nameof(ApplyEffect)}: Reduced speed to {target.Meta.Speed}.");
        }

        public override void OnExpire(MonsterBase target)
        {
            target.Meta.Speed = _baseSpeed;
            _diagnostics.AddCheck($"{nameof(FearEffect)}.{nameof(OnExpire)}: gets back normal speed {target.Meta.Speed}.");
        }      
    }
}
