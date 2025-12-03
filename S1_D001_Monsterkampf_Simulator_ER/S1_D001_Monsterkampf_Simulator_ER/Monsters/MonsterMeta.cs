/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : MonsterMeta.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Stores core monster stats such as HP, AP, DP and Speed. Every monster
*   instance holds one Meta object which is used during battle calculations.
*
* Responsibilities :
*   - Hold primary combat stats
*   - Allow modification during battle (damage, buffs, debuffs)
*   - Provide a central data structure for DamagePipeline & StatusEffects
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

namespace S1_D001_Monsterkampf_Simulator_ER.Monsters
{
    internal class MonsterMeta
    {
        /// <summary>
        /// Maximum HP of the monster.
        /// </summary>
        public float MaxHP { get; set; }

        /// <summary>
        /// Current HP of the monster. Can be modified during battle.
        /// </summary>
        public float CurrentHP { get; set; }

        /// <summary>
        /// Attack Power (AP). Used to calculate raw damage.
        /// </summary>
        public float AP { get; set; }

        /// <summary>
        /// Defense Power (DP). Reduces incoming damage.
        /// </summary>
        public float DP { get; set; }

        /// <summary>
        /// Determines battle turn order. Higher = faster.
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// Creates a new set of monster meta stats.
        /// </summary>
        /// <param name="maxHp">Maximum HP value.</param>
        /// <param name="currentHp">Current HP value.</param>
        /// <param name="ap">Attack Power.</param>
        /// <param name="dp">Defense Power.</param>
        /// <param name="speed">Turn order speed.</param>
        public MonsterMeta(float maxHp, float currentHp, float ap, float dp, float speed)
        {
            MaxHP = maxHp;
            CurrentHP = currentHp;
            AP = ap;
            DP = dp;
            Speed = speed;
        }
    }
}