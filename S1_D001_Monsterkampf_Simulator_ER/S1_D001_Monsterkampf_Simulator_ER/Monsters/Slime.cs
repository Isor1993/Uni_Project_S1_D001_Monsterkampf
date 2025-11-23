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

namespace S1_D001_Monsterkampf_Simulator_ER.Monsters
{
    internal class Slime : MonsterBase
    {
        // === Dependencies ===

        // === Fields ===    }

        public override string Description => "A weak but resilient creature made of gelatin with low damage.";

        public Slime(MonsterMeta meta, MonsterResistance resistance, int level, SkillPackage skill, DiagnosticsManager diagnostics)
            : base(
                 meta,
                 resistance,
                 RaceType.Slime,
                 level,
                 skill,
                 diagnostics)
        {
        }

        public override void Spawn()
        {
            if (SkillPackage.PassiveSkill != null)
            {
                UsePasiveSkill();
                _diagnostics.AddCheck($"{nameof(Slime)}.{nameof(Spawn)}: Activated passive skill '{SkillPackage.PassiveSkill.Name}'.");
            }
            else
            {
                _diagnostics.AddError($"{nameof(Slime)}.{nameof(Spawn)}: No passive skill assigned.");
            }
        }

        public string slime = @"                                                                                              
             ____     
            (    ) 
           (   O O)
          (    __ )
          (    V V)
          (________)
    ";
        public string slime2 = @"
            ____
           (    )
          (O O   )
          (__     )
          (V V    )
         (________) 
        ";
    }
}



