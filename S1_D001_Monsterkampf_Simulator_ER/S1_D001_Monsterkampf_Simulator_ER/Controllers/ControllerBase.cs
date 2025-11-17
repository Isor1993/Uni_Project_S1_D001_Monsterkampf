/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : ControllerBase.cs
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
* Base class for any battle controller that decides how a monster acts
* during its turn (player-controlled or AI-controlled).
*
* History :
* xx.xx.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Skills;

namespace S1_D001_Monsterkampf_Simulator_ER.Controllers
{
    internal class ControllerBase
    {
        // === Dependencies ===
        protected readonly DiagnosticsManager _diagnostics;

        // === Fields ===
        public MonsterBase Monster { get; }

        public ControllerBase(MonsterBase monster,DiagnosticsManager diagnostics)
        {
            Monster = monster ?? throw new ArgumentNullException(nameof(monster));
            _diagnostics = diagnostics ?? throw new ArgumentNullException(nameof(diagnostics));

        }

        /// <summary>
        /// Decides which skill this controller wants to use for its monster.
        /// The actual damage execution is handled by the BattleManager / DamagePipeline.
        /// </summary>
        public abstract SkillBase ChooseSkill();
    }
}
