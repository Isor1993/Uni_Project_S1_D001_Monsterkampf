using S1_D001_Monsterkampf_Simulator_ER.Balancing;
using S1_D001_Monsterkampf_Simulator_ER.Controllers;
using S1_D001_Monsterkampf_Simulator_ER.Factories;
using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;


namespace S1_D001_Monsterkampf_Simulator_ER.Dependencies
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Battle"></param>
    /// <param name="Rules"></param>
    /// <param name="UI"></param>
    /// <param name="Input"></param>
    /// <param name="Random"></param>
    /// <param name="Diagnostics"></param>
    internal sealed record GameDependencies
    (
        BattleManager Battle,
        RulesManager Rules,
        UIManager UI,
        InputManager Input,
        RandomManager Random,
        DiagnosticsManager Diagnostics,
        PrintManager Print,
        PlayerController PlayerController,
        MonsterBalancing Balancing,
        EnemyController EnemyController,
        MonsterFactory MonsterFactory
   
    );
}

