/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : TribeScreamEffect.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   StatusEffect that increases a monster’s AP for a limited number of rounds.
*   The buff is applied once at the start of the affected monster’s turn
*   and removed when the duration expires.
*
* Responsibilities :
*   - Increase AP by a multiplier once when the effect becomes active
*   - Restore the original AP value when the effect expires
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;


namespace S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects
{
    internal class TribeScreamEffect : StatusEffectBase
    {
        // === Fields ===
        private readonly float _multiplier;
        private float _baseAp;
        private bool _applied = false;

        /// <summary>
        ///  Creates a new Tribe Scream status effect instance.
        /// </summary>
        /// <param name="multiplier">AP multiplier applied to the target.</param>
        /// <param name="duration">How many rounds the effect should last.</param>
        /// <param name="diagnostics">Diagnostics manager used for debug logging.</param>
        public TribeScreamEffect(float multiplier, int duration, DiagnosticsManager diagnostics)
            : base("Tribe Scream",duration,diagnostics)
        {
            _multiplier = multiplier;
            
        }

        /// <summary>
        /// Applies the AP buff at the start of the target's turn.
        /// This is only done once, even if the effect lasts for multiple rounds.
        /// </summary>
        /// <param name="target">The monster receiving the AP increase.</param>
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

        /// <summary>
        /// Restores the target's original AP once the effect expires.
        /// </summary>
        /// <param name="target">The monster losing the AP buff.</param>
        public override void OnExpire(MonsterBase target)
        {
            target.Meta.AP = _baseAp;
            _diagnostics.AddCheck($"{nameof(TribeScreamEffect)}.{nameof(OnExpire)}: Restored AP back to {_baseAp}.");
        }
    }
}