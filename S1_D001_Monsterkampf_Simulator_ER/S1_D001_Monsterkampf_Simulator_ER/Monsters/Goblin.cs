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
        public static readonly string[] GoblinSpriteP =
        {

                @"    ____",
                @"   (   O)_",
                @"   |    __)",
                @"   |  ,_|",
                @"    \_ _|      _",
                @"   _/ /__ _   / ) ",
                @"  |    \ \ \_/ / ",
                @"  |    |\ \_/ / ",
                @"  |    | \___/ ",
                @" /______\  ",
                @"|  |  |  | ",
                @"|__|  |__|",
                @"(___) (___)"
        };
        public static readonly string[] GoblinSpriteE =
        {
                @"         ____",
                @"       _(O   )",
                @"      (__    |",
                @"        |_,  |",
                @" _      |_ _/",
                @"( \   _ __\ \_ ",
                @" \ \_/ / /    |",
                @"  \ \_/ /|    |",
                @"   \___/ |    |",
                @"        /______\",
                @"       |  |  |  |",
                @"       |__|  |__|",
                @"     (___)  (___)",
        };

        public override string Description => "A fast and aggressive fighter. Loves quick strikes.";



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

            if (SkillPackage.PassiveSkill != null)
            {
                UsePasiveSkill();

                _diagnostics.AddCheck($"{nameof(Goblin)}.{nameof(Spawn)}: Activated passive skill '{SkillPackage.PassiveSkill.Name}'.");
            }
            else
            {
                _diagnostics.AddError($"{nameof(Goblin)}.{nameof(Spawn)}: No passive skill assigned.");
            }
        }
        

    }
}
