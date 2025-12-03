/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : ControllerBase.cs
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
* Base class for all controllers (Player & Enemy).
* Provides shared logic for:
* - Holding a reference to the assigned monster
* - Selecting skills (abstract)
* - Reassigning a monster during runtime
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

        /// <summary>
        /// The monster the controller is currently managing.
        /// </summary>
        public MonsterBase Monster { get; private set; }

        /// <summary>
        /// Base constructor for all controllers.
        /// Stores monster reference and diagnostics dependency.
        /// </summary>
        /// <param name="monster">The monster this controller manages.</param>
        /// <param name="diagnostics">Diagnostics system for debug logging.</param>
        /// <exception cref="ArgumentNullException">Thrown if diagnostics is null.</exception>
        protected ControllerBase(MonsterBase monster, DiagnosticsManager diagnostics)
        {
            Monster = monster;
            _diagnostics = diagnostics ?? throw new ArgumentNullException(nameof(diagnostics));
        }

        /// <summary>
        /// Forces child classes (Player/Enemy) to implement their own
        /// skill-selection logic.
        /// </summary>
        /// <returns>The selected skill for this turn.</returns>
        public abstract SkillBase ChooseSkill();

        /// <summary>
        /// Reassigns the controller to a new monster (used between stages).
        /// Logs the selection via diagnostics.
        /// </summary>
        /// <param name="monster">The new monster instance.</param>
        /// <exception cref="ArgumentNullException">Thrown if monster is null.</exception>
        public void SetMonster(MonsterBase monster)
        {
            Monster = monster ?? throw new ArgumentNullException(nameof(monster));
            _diagnostics.AddCheck($"{nameof(ControllerBase)}.{nameof(SetMonster)}: Selected monster is {monster.Race}");
        }
    }
}