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
using System.Runtime.Intrinsics.X86;

namespace S1_D001_Monsterkampf_Simulator_ER.Monsters
{
    internal class Orc : MonsterBase
    {
        // === Dependencies ===

        // === Fields ===



        public Orc(MonsterMeta meta, MonsterResistance resistance, int level, SkillPackage skill, DiagnosticsManager diagnostics)
            : base(
                 meta,
                 resistance,
                 RaceType.Orc,
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

                _diagnostics.AddCheck($"{nameof(Orc)}.{nameof(Spawn)}: Activated passive skill '{Skills.PassiveSkill.Name}'.");
            }
            else
            {
                _diagnostics.AddError($"{nameof(Orc)}.{nameof(Spawn)}: No passive skill assigned.");
            }
        }
        public override void Attack(MonsterBase target)
        {
            throw new NotImplementedException();
        }

    }
}
