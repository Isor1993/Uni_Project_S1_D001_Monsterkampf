/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : MonsterResistance.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Stores all elemental and physical resistance values for a monster.
*   Each value represents the percentage of incoming damage reduced by that type.
*
* Responsibilities :
*   - Hold resistance percentages for Fire, Water, Physical and Poison damage
*   - Provide clean and simple access to these values
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

namespace S1_D001_Monsterkampf_Simulator_ER.Monsters
{
    internal class MonsterResistance
    {
        // === Fields ===

        /// <summary>
        /// Percentage of fire damage reduced (0–1 range).
        /// </summary>
        public float Fire { get; set; }

        /// <summary>
        /// Percentage of water damage reduced (0–1 range).
        /// </summary>
        public float Water { get; set; }

        /// <summary>
        /// Percentage of physical damage reduced (0–1 range).
        /// </summary>
        public float Physical { get; set; }

        /// <summary>
        /// Percentage of poison damage reduced (0–1 range).
        /// </summary>
        public float Poison { get; set; }

        /// <summary>
        /// Creates a new resistance profile for a monster.
        /// </summary>
        /// <param name="fire">Fire resistance (0–1).</param>
        /// <param name="water">Water resistance (0–1).</param>
        /// <param name="physical">Physical resistance (0–1).</param>
        /// <param name="poison">Poison resistance (0–1).</param>
        public MonsterResistance(float fire, float water, float physical, float poison)
        {
            Fire = fire;
            Water = water;
            Physical = physical;
            Poison = poison;
        }
    }
}