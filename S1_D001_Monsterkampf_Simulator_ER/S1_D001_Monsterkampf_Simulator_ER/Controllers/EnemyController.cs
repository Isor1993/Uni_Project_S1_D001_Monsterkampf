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
using S1_D001_Monsterkampf_Simulator_ER.Skills;

namespace S1_D001_Monsterkampf_Simulator_ER.Controllers
{
    internal class EnemyController:ControllerBase
    {
        // === Dependencies ===       
        private readonly RandomManager _random;
       
        // === Fields ===

        public EnemyController(MonsterBase monster,DiagnosticsManager diagnostics,RandomManager random):base(monster, diagnostics)
        {
            _random = random;
        }

        public override SkillBase ChooseSkill()
        {
            return Monster.ChooseSkillForAI(_random);
        }
    }
}
