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



using System.Diagnostics;

namespace S1_D001_Monsterkampf_Simulator_ER.Monsters
{

    internal class MonsterMeta
    {

        public float HP { get; set; }
        public float AP { get; set; }
        public float DP { get; set; }
        public float Speed { get; set; }
      

        public MonsterMeta(float hp, float ap, float dp, float speed)
        {
            HP = hp;
            AP = ap;
            DP = dp;
            Speed = speed;     
        }
    }
}
