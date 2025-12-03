/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : Troll.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Represents the Troll monster. A slow but durable creature that regenerates
*   health naturally and uses powerful physical attacks.
*
* Responsibilities :
*   - Define Troll-specific sprites and descriptions
*   - Activate Troll passive skills on spawn
*   - Render Troll ASCII sprites depending on side (player/enemy)
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Skills;

namespace S1_D001_Monsterkampf_Simulator_ER.Monsters
{
    internal class Troll : MonsterBase
    {
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
            @"            /______\",
            @"           |  |  |  |",
            @"           |__|  |__|",
            @"          (___) (___)",
        };

        /// <summary>
        /// Short description shown in UI and debug output.
        /// </summary>
        public override string Description => "A slow but extremely durable monster. Regenerates health naturally.";

        /// <summary>
        /// Creates a new Troll monster instance.
        /// </summary>
        /// <param name="meta">Meta stats (HP, AP, DP, Speed).</param>
        /// <param name="resistance">Elemental resistance values.</param>
        /// <param name="level">Monster level.</param>
        /// <param name="skill">Skill package containing active & passive skills.</param>
        /// <param name="diagnostics">Diagnostics manager for debug logging.</param>
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

        /// <summary>
        /// Called when the troll enters a battle.
        /// Activates its passive skill if available.
        /// </summary>
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

        /// <summary>
        /// Renders the Troll ASCII sprite depending on whether it's the player
        /// or the enemy.
        /// </summary>
        /// <param name="isPlayer">True if the Troll is controlled by the player.</param>
        public override void PrintSprite(bool isPlayer)
        {
            int posX = 0;
            int posY = 9
                ;
            if (isPlayer)
            {
                posX = 27;
                for (int i = 0; i < TrollSpriteP.Length; i++)
                {
                    Console.SetCursorPosition(posX, posY + i);
                    Console.Write(TrollSpriteP[i]);
                }
            }
            else
            {
                posX = 70;
                for (int i = 0; i < TrollSpriteE.Length; i++)
                {
                    Console.SetCursorPosition(posX, posY + i);
                    Console.Write(TrollSpriteE[i]);
                }
            }
        }
    }
}