/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : Goblin.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Represents the Goblin monster. Fast, aggressive and specialized in
*   quick strikes. Uses physical attack skills and benefits from its
*   high speed in combat.
*
* Responsibilities :
*   - Provide Goblin-specific ASCII sprites (player/enemy)
*   - Apply Goblin passive skills on spawn
*   - Render sprite depending on owner (player or enemy)
*   - Define flavor description for UI / debugging
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Skills;

namespace S1_D001_Monsterkampf_Simulator_ER.Monsters
{
    internal class Goblin : MonsterBase
    {
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

        /// <summary>
        /// Short flavor description shown in UI and logs.
        /// </summary>
        public override string Description => "A fast and aggressive fighter. Loves quick strikes.";

        /// <summary>
        /// Creates a new Goblin monster.
        /// </summary>
        /// <param name="meta">Meta stats (HP, AP, DP, Speed).</param>
        /// <param name="resistance">Elemental resistance values.</param>
        /// <param name="level">Goblin’s level.</param>
        /// <param name="skill">Skill package containing active & passive skills.</param>
        /// <param name="diagnostics">Diagnostics manager for debug logging.</param>
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

        /// <summary>
        /// Called when the Goblin enters a battle.
        /// Activates its passive skill if assigned.
        /// </summary>
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

        /// <summary>
        /// Renders the Goblin ASCII sprite depending on whether it belongs to
        /// the player or the enemy.
        /// </summary>
        /// <param name="isPlayer">True if the Goblin belongs to the player.</param>
        public override void PrintSprite(bool isPlayer)
        {
            int posX = 0;
            int posY = 10;
            if (isPlayer)
            {
                posX = 27;
                for (int i = 0; i < GoblinSpriteP.Length; i++)
                {
                    Console.SetCursorPosition(posX, posY + i);
                    Console.Write(GoblinSpriteP[i]);
                }
            }
            else
            {
                posX = 70;
                for (int i = 0; i < GoblinSpriteE.Length; i++)
                {
                    Console.SetCursorPosition(posX, posY + i);
                    Console.Write(GoblinSpriteE[i]);
                }
            }
        }
    }
}