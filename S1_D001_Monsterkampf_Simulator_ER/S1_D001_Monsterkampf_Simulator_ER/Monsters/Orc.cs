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
        public override string Description => "A brute with overwhelming physical strength. High AP, low speed.";


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
            if (SkillPackage.PassiveSkill != null)
            {
                UsePasiveSkill();

                _diagnostics.AddCheck($"{nameof(Orc)}.{nameof(Spawn)}: Activated passive skill '{SkillPackage.PassiveSkill.Name}'.");
            }
            else
            {
                _diagnostics.AddError($"{nameof(Orc)}.{nameof(Spawn)}: No passive skill assigned.");
            }
        }
        private readonly string[] OrcSpriteP =
        {

            @"   A__A",
            @"  (   O)_",
            @"  |    __)",
            @"  | A_A|",
            @"   \_ _|        ____  ",
            @" ___/ /____     /   /",
            @"(        \ \   /   /",
            @"|       \ \ \_/ __/ ",
            @"|        \ \_/ / ",
            @"|        )\___/ ",
            @" \      _) ",
            @" /______\  ",
            @"|  |  |  | ",
            @"|__|  |__|",
            @"(___) (___)"
        };
        private readonly string[] OrcSpriteE =
        {
            @"              A__A",
            @"            _(O   )",
            @"           (__    |",
            @"            |A_A  |",
            @"____        |_ _/",
            @"\   \     ____\ \___ ",
            @" \   \   / /        )  ",
            @"  \__ \_/ / /       |",
            @"     \ \_/ /        |",
            @"      \___/(        |",
            @"           (_      /",
            @"            /______\",
            @"           |  |  |  |",
            @"           |__|  |__|",
            @"          (___) (___)",
        };
    }
}
}
