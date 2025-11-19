/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : EnemyController.cs
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
* Controller used for enemy AI. Delegates skill selection to the AI logic
* inside the associated MonsterBase.
*
* History :
* xx.xx.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Skills;
using System;

namespace S1_D001_Monsterkampf_Simulator_ER.Controllers
{
    internal class EnemyController:ControllerBase
    {
        // === Dependencies ===       
        private readonly RandomManager _random;
       
        // === Fields ===

        public EnemyController(MonsterBase monster,DiagnosticsManager diagnostics,RandomManager random):base(monster, diagnostics)
        {
            _random = random?? throw new ArgumentNullException(nameof(random));
        }

        public override SkillBase ChooseSkill()
        {
            SkillBase chosen = Monster.ChooseSkillForAI(_random);

            _diagnostics.AddCheck($"{nameof(EnemyController)}.{nameof(ChooseSkill)}: {Monster.Race} AI selected '{chosen.Name}'.");

            return chosen;
        }
        
        public void SetMonster(MonsterBase controller)
        {

        }
    }
}
