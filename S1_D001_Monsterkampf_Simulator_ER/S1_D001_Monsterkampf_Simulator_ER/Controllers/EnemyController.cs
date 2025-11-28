/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : EnemyController.cs
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
* Controls the AI logic for enemy skill selection.
* - Uses RandomManager to choose a valid skill
* - Logs all selections for diagnostics
* - Provides SetMonster override for dynamic reassignment
*
* History :
* xx.xx.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Skills;

namespace S1_D001_Monsterkampf_Simulator_ER.Controllers
{
    internal class EnemyController : ControllerBase
    {
        // === Dependencies ===       
        private readonly RandomManager _random;

        /// <summary>
        /// Constructs a new EnemyController with required dependencies.
        /// </summary>
        /// <param name="monster">The monster controlled by the AI.</param>
        /// <param name="diagnostics">Diagnostics manager for logging.</param>
        /// <param name="random">RandomManager for weighted/randomized AI decisions.</param>
        /// <exception cref="ArgumentNullException">Thrown if any dependency is null.</exception>
        public EnemyController(MonsterBase monster, DiagnosticsManager diagnostics, RandomManager random) : base(monster, diagnostics)
        {
            _random = random ?? throw new ArgumentNullException(nameof(random));
        }

        /// <summary>
        /// Selects a skill using the monster’s AI logic.
        /// </summary>
        /// <returns>The chosen skill instance.</returns>
        public override SkillBase ChooseSkill()
        {
            SkillBase chosen = Monster.ChooseSkillForAI(_random);

            _diagnostics.AddCheck($"{nameof(EnemyController)}.{nameof(ChooseSkill)}: {Monster.Race} AI selected '{chosen.Name}'.");

            return chosen;
        }

        /// <summary>
        /// Assigns a new monster to this controller.
        /// </summary>
        /// <param name="monster">The new monster instance.</param>
        public void SetMonster(MonsterBase monster)
        {
            base.SetMonster(monster);
        }
    }
}
