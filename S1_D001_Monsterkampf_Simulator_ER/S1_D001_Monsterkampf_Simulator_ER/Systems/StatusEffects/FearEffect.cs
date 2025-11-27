/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : FearEffect.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Permanent status effect that reduces the monster's Speed stat.
*   The reduction is applied once when the target's first turn begins.
*
* Responsibilities :
*   - Reduce speed by a multiplier
*   - Restore original speed when the effect expires
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;

namespace S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects
{
    internal class FearEffect : PermanentStatusEffect
    {
        // === Fields ===
        private readonly float _multiplier;
        private float _baseSpeed;
        private bool _applied = false;

        /// <summary>
        /// Creates a new fear status effect instance.
        /// </summary>
        /// <param name="multiplier">Multiplier used to reduce the target's speed.</param>
        /// <param name="diagnostics">Diagnostics manager used for debug logging.</param>
        public FearEffect(float multiplier, DiagnosticsManager diagnostics)
           : base("Fear",diagnostics)
        {
            _multiplier = multiplier;
        }

        /// <summary>
        /// Applies the speed reduction at the start of the target's turn.
        /// This is only done once because this is a permanent effect.
        /// </summary>
        /// <param name="target">The monster receiving the speed reduction.</param>
        public override void ApplyStartOfTurn(MonsterBase target)
        {
            if (_applied)
            {
                return;
            }
            _applied = true;

            _baseSpeed = target.Meta.Speed;
            target.Meta.Speed = Math.Max(1, target.Meta.Speed * _multiplier);
            float percent = (1f - _multiplier) * 100f;

            _diagnostics.AddCheck($"{nameof(FearEffect)}.{nameof(ApplyStartOfTurn)}: Reduced speed by {percent:F0}% → New Speed = {target.Meta.Speed}.");
        }

        /// <summary>
        /// Restores the target's original speed when the effect expires.
        /// </summary>
        /// <param name="target">The monster regaining its original speed.</param>
        public override void OnExpire(MonsterBase target)
        {
            target.Meta.Speed = _baseSpeed;
            _diagnostics.AddCheck($"{nameof(FearEffect)}.{nameof(OnExpire)}: Speed restored to {target.Meta.Speed}.");
        }
    }
}
