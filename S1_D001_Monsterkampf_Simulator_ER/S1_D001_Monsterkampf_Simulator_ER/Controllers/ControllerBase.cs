/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : ControllerBase.cs
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
* Base class for all controllers (Player & Enemy).
* Provides core references and a unified interface.
*
* History :
* xx.xx.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Skills;

namespace S1_D001_Monsterkampf_Simulator_ER.Controllers
{
    internal abstract class ControllerBase
    {
        // === Dependencies ===
        protected readonly DiagnosticsManager _diagnostics;

        // === Fields ===
        public MonsterBase Monster { get; }

        protected ControllerBase(MonsterBase monster,DiagnosticsManager diagnostics)
        {
            Monster = monster ?? throw new ArgumentNullException(nameof(monster));
            _diagnostics = diagnostics ?? throw new ArgumentNullException(nameof(diagnostics));

        }

        /// <summary>
        /// Must be implemented by PlayerController and EnemyController.
        /// Returns a valid SkillBase object chosen for this turn.
        /// </summary>
        public abstract SkillBase ChooseSkill();
    }
}
