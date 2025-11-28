/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : Slime.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Represents the Slime monster. A weak but resilient creature made of
*   gelatin. Uses elemental water/fire attacks and may absorb incoming damage
*   through passive skills.
*
* Responsibilities :
*   - Define Slime-specific sprites and descriptions
*   - Activate Slime passive skill on spawn
*   - Render Slime ASCII sprites depending on side (player/enemy)
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Skills;

namespace S1_D001_Monsterkampf_Simulator_ER.Monsters
{
    internal class Slime : MonsterBase
    {

        // === Fields ===    }

        public static readonly string[] SlimeSpriteP =
        {
            "     ____",
            "    (    )",
            "   (   O O)",
            "  (    __ )",
            "  (    V V)",
            "  (________)"
        };

        public static readonly string[] SlimeSpriteE =
        {
            "     ____",
            "    (    )",
            "   (O O   )",
            "   (__     )",
            "   (V V    )",
            "  (________)"
        };

        /// <summary>
        /// Short description displayed in UI and debug output.
        /// </summary>
        public override string Description => "A weak but resilient creature made of gelatin with low damage.";

        /// <summary>
        /// Creates a new Slime monster instance.
        /// </summary>
        /// <param name="meta">Meta stats (HP, AP, DP, Speed).</param>
        /// <param name="resistance">Elemental resistance values.</param>
        /// <param name="level">Slime’s level.</param>
        /// <param name="skill">Skill package containing active & passive skills.</param>
        /// <param name="diagnostics">Diagnostics manager for debug logging.</param>
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

        /// <summary>
        /// Called when the Slime enters a battle.
        /// Activates its passive skill if available.
        /// </summary>
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

        /// <summary>
        /// Renders the Slime ASCII sprite depending on whether it is
        /// the player or the enemy.
        /// </summary>
        /// <param name="isPlayer">True if the Slime belongs to the player.</param>
        public override void PrintSprite(bool isPlayer)
        {
            int posX = 0;
            int posY = 17;
            if (isPlayer)
            {
                posX = 27;
                for (int i = 0; i < SlimeSpriteP.Length; i++)
                {
                    Console.SetCursorPosition(posX, posY + i);
                    Console.Write(SlimeSpriteP[i]);
                }
            }
            else
            {
                posX = 70;
                for (int i = 0; i < SlimeSpriteE.Length; i++)
                {
                    Console.SetCursorPosition(posX, posY + i);
                    Console.Write(SlimeSpriteE[i]);
                }
            }
        }
    }
}



