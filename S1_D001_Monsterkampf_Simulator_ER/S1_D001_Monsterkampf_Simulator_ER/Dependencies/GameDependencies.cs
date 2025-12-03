/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : GameDependencies.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Central dependency container holding all major game systems, controllers,
*   managers, factories and pipelines. This record is injected into GameManager
*   and allows clean access to all required components without tight coupling.
*
* Responsibilities :
*   - Provide a structured bundle of all core systems (UI, Input, Factory, etc.)
*   - Serve as single source for dependency injection across the game
*   - Ensure all managers & controllers can be passed around efficiently
*
* History :
*   03.12.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Balancing;
using S1_D001_Monsterkampf_Simulator_ER.Controllers;
using S1_D001_Monsterkampf_Simulator_ER.Factories;
using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Systems.Damage;

namespace S1_D001_Monsterkampf_Simulator_ER.Dependencies
{
    /// <summary>
    /// Immutable dependency container for all global game systems.
    /// Passed into the GameManager to supply everything required for running the game.
    /// </summary>
    /// <param name="DamagePipeline">
    ///     The shared DamagePipeline used for applying damage steps,
    ///     resistances, status effects, mitigation and final damage modification.
    /// </param>
    /// <param name="UI">
    ///     The UIManager responsible for rendering all in-fight and menu UI
    ///     (skill boxes, monster boxes, message boxes, stat choice windows, etc.).
    /// </param>
    /// <param name="Input">
    ///     Raw player input layer (keyboard abstraction) returning PlayerCommand values.
    /// </param>
    /// <param name="InputManager">
    ///     Higher-level input handler used for pointer navigation and menu confirmation.
    /// </param>
    /// <param name="Random">
    ///     RandomManager used for enemy selection, damage previews, and RNG-based AI decisions.
    /// </param>
    /// <param name="Diagnostics">
    ///     DiagnosticsManager for logging checks, errors, and debugging notes.
    /// </param>
    /// <param name="Print">
    ///     Simple print output manager for debug lines or general console messages.
    /// </param>
    /// <param name="PlayerController">
    ///     Controller responsible for managing the player monster during battle.
    /// </param>
    /// <param name="Balancing">
    ///     MonsterBalancing instance containing scaling rules, meta-data and resistances.
    /// </param>
    /// <param name="EnemyController">
    ///     Controller managing the enemy monster during battle.
    /// </param>
    /// <param name="MonsterFactory">
    ///     Factory used to construct fully-configured monster instances.
    /// </param>
    /// <param name="Screen">
    ///     ScreenManager responsible for Start, Tutorial and End screens including ASCII graphics.
    /// </param>
    internal sealed record GameDependencies
    (
        DamagePipeline DamagePipeline,
        UIManager UI,
        IPlayerInput Input,
        InputManager InputManager,
        RandomManager Random,
        DiagnosticsManager Diagnostics,
        PrintManager Print,
        PlayerController PlayerController,
        MonsterBalancing Balancing,
        EnemyController EnemyController,
        MonsterFactory MonsterFactory,
        ScreenManager Screen
    );
}