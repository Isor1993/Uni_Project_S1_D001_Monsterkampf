using S1_D001_Monsterkampf_Simulator_ER.Managers;
using Semester1_D001_Escape_Room_Rosenberg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    internal sealed record GameDependencies(
    BattleManager Battle,
    RulesManager Rules,
    UIManager UI,
    InputManager Input,
    RandomManager Random,
    DiagnosticsManager Diagnostics,
    PrintManager Print
);
}

