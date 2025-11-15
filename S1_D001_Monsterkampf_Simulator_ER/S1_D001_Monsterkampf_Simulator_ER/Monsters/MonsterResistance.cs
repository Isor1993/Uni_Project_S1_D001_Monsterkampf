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

using S1_D001_Monsterkampf_Simulator_ER.Skills.Slime;

namespace S1_D001_Monsterkampf_Simulator_ER.Monsters
{
    internal class MonsterResistance
    {
        // === Dependencies ===

        // === Fields ===

        public float Fire {  get; set; }


        public float Water {  get; set; }


        public float Physical {  get; set; }


        public float Poison { get; set; }


        public MonsterResistance( float fire, float water, float physical, float poison)
        {
            Fire = fire;
            Water = water;
            Physical = physical;
            Poison = poison;
        }

    }
}
