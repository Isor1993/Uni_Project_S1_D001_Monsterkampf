/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : Orc.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Represents the Orc monster. A physically powerful brute with very high AP
*   but low speed. Uses strong offensive skills and gains benefits from its
*   passive abilities.
*
* Responsibilities :
*   - Define Orc-specific ASCII sprites and description
*   - Activate Orc passive skills on spawn
*   - Render Orc ASCII sprite (player/enemy variants)
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Skills;

namespace S1_D001_Monsterkampf_Simulator_ER.Monsters
{
    internal class Orc : MonsterBase
    {
        // === Fields ===

        public static readonly string[] OrcSpriteP =
        {

            @"   A___A",
            @"  (    O)_",
            @"  |     __)",
            @"  |  A_A|",
            @"   \_  _|        ____  ",
            @"  __/ /____     /   /",
            @" (       \ \   /   /",
            @" |      \ \ \_/ __/ ",
            @" |      |\ \_/ / ",
            @" |      ) \___/ ",
            @" /______\  ",
            @"|  |  |  | ",
            @"|__|  |__|",
            @"(___) (___)"
        };

        public static readonly string[] OrcSpriteE =
        {
            @"             A___A",
            @"           _(O    )",
            @"          (__     |",
            @"            |A_A  |",
            @"____        |_  _/",
            @"\   \     ____\ \__ ",
            @" \   \   / /       )  ",
            @"  \__ \_/ / /      |",
            @"     \ \_/ /|      |",
            @"      \___/ (      |",
            @"            /______\",
            @"           |  |  |  |",
            @"           |__|  |__|",
            @"          (___) (___)",
        };

        /// <summary>
        /// Short flavor description displayed in the UI and debug output.
        /// </summary>
        public override string Description => "A brute with overwhelming physical strength. High AP, low speed.";

        /// <summary>
        /// Creates a new Orc monster instance.
        /// </summary>
        /// <param name="meta">Meta stats (HP, AP, DP, Speed).</param>
        /// <param name="resistance">Elemental resistance values.</param>
        /// <param name="level">Monster level.</param>
        /// <param name="skill">Skill package containing active & passive skills.</param>
        /// <param name="diagnostics">Diagnostics manager for debug logging.</param>
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

        /// <summary>
        /// Called when the Orc enters a battle.
        /// Activates its passive skill if available.
        /// </summary>
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

        /// <summary>
        /// Renders the Orc ASCII sprite on screen.
        /// </summary>
        /// <param name="isPlayer">True if rendered on the player's side.</param>
        public override void PrintSprite(bool isPlayer)
        {
            int posX = 0;
            int posY = 9;
            if (isPlayer)
            {
                posX = 27;
                for (int i = 0; i < OrcSpriteP.Length; i++)
                {
                    Console.SetCursorPosition(posX, posY + i);
                    Console.Write(OrcSpriteP[i]);
                }
            }
            else
            {
                posX = 70;
                for (int i = 0; i < OrcSpriteE.Length; i++)
                {
                    Console.SetCursorPosition(posX, posY + i);
                    Console.Write(OrcSpriteE[i]);
                }
            }
        }
    }
}

