/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : BattleManagerDependencies.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Dependency container for the BattleManager. Bundles all required managers,
*   controllers, balancing data, player data and utility classes into a single,
*   immutable record used during battle.
*
* Responsibilities :
*   - Provide all systems needed for running a full turn-based battle
*   - Supply Player- and EnemyController for monster control
*   - Provide balancing data, diagnostics and UI access
*   - Expose RandomManager and DamagePipeline to the BattleManager
*   - Ensure type-safe and clean dependency injection
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Balancing;
using S1_D001_Monsterkampf_Simulator_ER.Controllers;
using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Player;
using S1_D001_Monsterkampf_Simulator_ER.Systems.Damage;

namespace S1_D001_Monsterkampf_Simulator_ER.Dependencies
{
    /// <summary>
    /// Immutable dependency bundle required by the BattleManager.
    /// Contains all controllers, logic modules, balancing data,
    /// PlayerData reference and UI systems needed to execute a full battle.
    /// </summary>
    /// <param name="PlayerController">
    /// The controller responsible for player monster actions.
    /// </param>
    /// <param name="EnemyController">
    /// The controller responsible for enemy AI monster actions.
    /// </param>
    /// <param name="Diagnostics">
    /// Global diagnostics manager used for logging battle events.
    /// </param>
    /// <param name="Random">
    /// RandomManager used for skill choices and probabilistic effects.
    /// </param>
    /// <param name="Pipeline">
    /// The complete damage pipeline used to compute final damage values.
    /// </param>
    /// <param name="PlayerData">
    /// Player progression data (stat points, completed battles, etc.).
    /// </param>
    /// <param name="Balancing">
    /// Balancing configuration used for scaling, damage rules, etc.
    /// </param>
    /// <param name="UI">
    /// UIManager for updating all in-battle UI boxes and messages.
    /// </param>
    internal record BattleManagerDependencies
    (
        ControllerBase PlayerController,
        ControllerBase EnemyController,
        DiagnosticsManager Diagnostics,
        RandomManager Random,
        DamagePipeline Pipeline,
        PlayerData PlayerData,
        MonsterBalancing Balancing,
        UIManager UI
    );
}