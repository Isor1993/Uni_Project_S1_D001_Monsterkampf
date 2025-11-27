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
    internal class Troll : MonsterBase
    {
        // === Dependencies ===

        // === Fields ===
        public static readonly string[] TrollSpriteP =
        {
            @"   A___A",
            @"  (    O)_",
            @"  |     __)",
            @"  |   ,_|",
            @"   \_  _|       ____  ",
            @"   _/ /___     /   /",
            @"  |     \ \   /   /",
            @"  |    \ \ \_/ __/ ",
            @"  |    |\ \_/ / ",
            @"  |    | \___/ ",
            @"  |    |  ",
            @" /______\  ",
            @"|  |  |  | ",
            @"|__|  |__|",
            @"(___) (___)"
        };

        public static readonly string[] TrollSpriteE =
        {
            @"             A___A",
            @"           _(O    )",
            @"          (__     |",
            @"            |_,   |",
            @" ____       |_  _/",
            @" \   \     ___\ \_",
            @"  \   \   / /     |  ",
            @"   \__ \_/ / /    |",
            @"      \ \_/ /|    |",
            @"       \___/ |    |",
            @"             |    |",
            @"            /______\",
            @"           |  |  |  |",
            @"           |__|  |__|",
            @"          (___) (___)",
        };

        public override string Description => "A slow but extremely durable monster. Regenerates health naturally.";

        public Troll(MonsterMeta meta, MonsterResistance resistance, int level, SkillPackage skill, DiagnosticsManager diagnostics)
            : base(
                 meta,
                 resistance,
                 RaceType.Troll,
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
                _diagnostics.AddCheck($"{nameof(Troll)}.{nameof(Spawn)}: Activated passive skill '{SkillPackage.PassiveSkill.Name}'.");
            }
            else
            {
                _diagnostics.AddError($"{nameof(Troll)}.{nameof(Spawn)}: No passive skill assigned.");
            }
        }
    }
}