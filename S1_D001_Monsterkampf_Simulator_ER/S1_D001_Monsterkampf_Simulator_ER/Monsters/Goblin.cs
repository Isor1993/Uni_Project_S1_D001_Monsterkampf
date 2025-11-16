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
using S1_D001_Monsterkampf_Simulator_ER.Skills;
using S1_D001_Monsterkampf_Simulator_ER.Skills.Goblin;
using System.Security.Cryptography.X509Certificates;

namespace S1_D001_Monsterkampf_Simulator_ER.Monsters
{
    internal class Goblin : MonsterBase
    {
        // === Dependencies ===

        // === Fields ===



        public Goblin(MonsterMeta meta, MonsterResistance resistance, int level, SkillPackage skill, DiagnosticsManager diagnostics)
            : base(
                 meta,
                 resistance,
                 RaceType.Goblin,
                 level,
                 skill,
                 diagnostics)
        {

        }

        public override void Spawn()
        {

            if (Skills.PassiveSkill != null)
            {
                UsePasiveSkill();

                _diagnostics.AddCheck($"{nameof(Goblin)}.{nameof(Spawn)}: Activated passive skill '{Skills.PassiveSkill.Name}'.");
            }
            else
            {
                _diagnostics.AddError($"{nameof(Goblin)}.{nameof(Spawn)}: No passive skill assigned.");
            }
        }
        public override void Attack(MonsterBase target)
        {
            base.Attack(target);
        }
    }
}
