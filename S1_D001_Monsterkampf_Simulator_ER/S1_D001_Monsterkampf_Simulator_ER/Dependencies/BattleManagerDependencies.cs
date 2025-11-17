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
using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Systems.Damage;

namespace S1_D001_Monsterkampf_Simulator_ER.Dependencies
{
    internal record BattleManagerDependencies
    (
        MonsterBase Player,
        MonsterBase Enemy,
        DiagnosticsManager Diagnostics,
        RandomManager Random,
        DamagePipeline Pipeline
    );
}
