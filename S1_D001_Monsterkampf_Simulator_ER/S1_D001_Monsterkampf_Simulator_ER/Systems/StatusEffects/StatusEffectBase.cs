/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : StatusEffectBase.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Abstract base class for all status effects applied to monsters.
*   Provides shared properties such as Name and Duration, as well as
*   default behavior for turn-based execution and diagnostics logging.
*
* Responsibilities :
*   - Store status effect metadata (Name, Duration)
*   - Provide overridable turn event methods for derived effects
*   - Handle duration ticking and expiration logic
*   - Offer a hook to modify final damage values
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;

namespace S1_D001_Monsterkampf_Simulator_ER.Systems.StatusEffects
{
    internal abstract class StatusEffectBase
    {

        protected readonly DiagnosticsManager _diagnostics;

        /// <summary>
        /// Name of the effect.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Remaining rounds until the effect expires.
        /// </summary>
        public int Duration { get; protected set; }

        /// <summary>
        /// Creates a new base status effect instance.
        /// </summary>
        /// <param name="name">The name of the status effect.</param>
        /// <param name="duration">How many rounds the effect should last.</param>
        /// <param name="diagnosticsManager">Diagnostics manager used for debug logging.</param>
        protected StatusEffectBase(string name, int duration, DiagnosticsManager diagnosticsManager)
        {
            Name = name;
            Duration = duration;
            _diagnostics = diagnosticsManager;
        }

        /// <summary>
        /// Called at the start of the monster’s turn.
        /// Default implementation does nothing.
        /// </summary>
        /// <param name="target">The monster affected by the status effect.</param>
        public virtual void ApplyStartOfTurn(MonsterBase target)
        {
            
        }

        /// <summary>
        /// Called at the end of the monster’s turn.
        /// Default implementation does nothing.
        /// </summary>
        /// <param name="target">The monster affected by the status effect.</param>
        public virtual void ApplyEndOfTurn(MonsterBase target)
        {

        }

        /// <summary>
        /// Decreases the remaining duration of the effect by one round
        /// and writes a diagnostics log entry.
        /// </summary>
        public virtual void Tick()
        {
            Duration--;

            _diagnostics.AddCheck($"{nameof(StatusEffectBase)}.{nameof(Tick)}: Successfully substracted duration {Duration} from {Name}.");
        }

        /// <summary>
        /// Called when the duration reaches zero.
        /// </summary>
        /// <param name="target">The monster losing this status effect.</param>
        public virtual void OnExpire(MonsterBase target)
        {
            _diagnostics.AddCheck($"{nameof(StatusEffectBase)}.{nameof(OnExpire)}: {Name} expired on {target.Race}");
        }

        /// <summary>
        /// Indicates whether the effect has expired.
        /// </summary>
        public bool IsExpired => Duration <= 0;

        /// <summary>
        /// Allows derived classes to modify the final incoming damage.
        /// Default implementation does nothing.
        /// </summary>
        /// <param name="target">The monster receiving the damage.</param>
        /// <param name="damage">The current computed damage value.</param>
        /// <returns>The modified damage value.</returns>
        public virtual float ModifyFinalDamage(MonsterBase target, float damage)
        {
            //Later for something like debuff wich also inflict true damage
            return damage; // default: do nothing
        }
    }
}