# 🧩 Monsterkampf-Simulator -- Console Edition

*A modular C# console battle system built with clean architecture,
turn-based combat, and fully expandable monster design.*

![Gameplay
Screenshot](1_Monsterkampf/other/Screenshots/Screenshot_01.png)

------------------------------------------------------------------------

## 🎮 Overview

The **Monsterkampf-Simulator** is a fully modular **C# (.NET 8.0)**
console game created during my Game Programming studies at the **SAE
Institute Stuttgart**.\
It is a **1v1 turn-based battle system** where each monster has unique
stats, skills, passives, and status effects.\
The project demonstrates how to build a fully structured combat
framework even inside a simple console window.

------------------------------------------------------------------------

## ✨ Core Features

-   🐉 **4 Unique Monster Classes** -- Goblin, Orc, Troll, and Slime,
    each with unique stats and passives.\
-   🔥 **Active & Passive Skill System** -- Damage skills, elemental
    attacks, buffs, DOT, regeneration & fear effects.\
-   🧪 **Status Effect Framework** -- Poison, Absorb, Fear, TribeScream
    Buff, Regeneration & more.\
-   ⚔️ **Damage Pipeline Architecture** -- Raw Damage → Resistances →
    Status Mods → Final Damage.\
-   🧱 **Modular Manager Structure** -- Battle, UI, Input, Diagnostics,
    Factory, Random & more.\
-   🎛️ **Turn-Based Combat Loop** -- Start-of-turn, player action, enemy
    AI action, cooldown ticks.\
-   🖥️ **ASCII-Based UI** -- Player/Enemy stats, skill menus, message
    console, and monster sprites.\
-   🩻 **Diagnostics Logger** -- Tracks checks, warnings, and errors
    during gameplay.

------------------------------------------------------------------------

## 🕹️ How to Play

1.  Clone the repository:

    ``` bash
    git clone https://github.com/<yourusername>/Monsterkampf-Simulator.git
    ```

2.  Open the solution in **Visual Studio 2022**.\

3.  Run the game (`Ctrl + F5`).\

4.  Controls:

    -   `W / S` → Navigate skill list\
    -   `ENTER` → Confirm skill\
    -   `ESC` → Quit

> The game runs entirely in the console with dynamic box rendering and
> ASCII monsters.

------------------------------------------------------------------------

## 🧱 Architecture Breakdown

  ------------------------------------------------------------------------
  System                   Responsibility
  ------------------------ -----------------------------------------------
  **GameManager**          Overall game flow (start → battle → result →
                           loop).

  **BattleManager**        Turn flow, skill execution, damage pipeline,
                           status effects.

  **UIManager**            Draws all UI boxes (player, enemy, skills,
                           messages).

  **ScreenManager**        Handles start, tutorial, fight, and end
                           screens.

  **PlayerController**     Handles player skill selection and actions.

  **EnemyController**      AI skill choice based on cooldowns and power.

  **DamagePipeline**       Multi-step damage calculation.

  **MonsterFactory**       Creates monsters with stats, resistances, and
                           skills.

  **RandomManager**        Provides random values for combat actions.

  **DiagnosticsManager**   Timestamped logging (Checks, Warnings, Errors).

  **PrintManager**         Console cursor control & ASCII rendering.

  **SymbolManager**        Stores ASCII symbols used across the UI.
  ------------------------------------------------------------------------

------------------------------------------------------------------------

## 🧠 Design Principles

-   **SRP (Single Responsibility Principle)** -- Every manager has one
    job.\
-   **Dependency Injection** -- Clean separation & testability.\
-   **Expandable Design** -- New monsters, skills, effects can be added
    easily.\
-   **Turn-Based Clarity** -- Each round is predictable, logged, and
    structured.\
-   **Consistent UI Layout** -- Console is used as a structured grid
    rather than plain text.

------------------------------------------------------------------------

## ⚙️ Tech Stack

  Category       Tools
  -------------- --------------------------------------
  Language       C#
  Framework      .NET 8.0
  IDE            Visual Studio 2022
  Architecture   Modular / SRP / Dependency Injection
  Platform       Windows Console

------------------------------------------------------------------------

## 📂 Repository Structure

    1_Monsterkampf/
    │
    ├── src/                                     # Full source code
    │   ├── Monsters/
    │   ├── Skills/
    │   ├── Systems/
    │   ├── Managers/
    │   ├── Dependencies/
    │   ├── Factories/
    │   └── Program.cs
    │
    ├── other/                                   # Additional media and docs
    │   ├── Dokumente/
    │   │      ├── doku.pdf
    │   │      └── Extras and Old/
    │   │
    │   ├── Screenshots/
    │   │      ├── Screenshot_01.png
    │   │      ├── Screenshot_02.png
    │   │      └── ...
    │   │
    │   └── Gameplay.mp4
    │
    └── README.md                                # This file

------------------------------------------------------------------------

## 💬 Behind the Project

> "Monsterkampf-Simulator is a playground for building clean, expandable
> combat systems.\
> Every subsystem---skills, damage, UI, AI---was designed like an engine
> module, not a single assignment."

It combines **architecture discipline**, **gameplay design**, and
**console creativity**.

------------------------------------------------------------------------

## 🧾 License

This project is available under the **MIT License**.\
Feel free to learn from it, modify it, and extend it.

------------------------------------------------------------------------

## 📫 Contact

**Eric Rosenberg**\
🎓 Game Programming Student -- SAE Institute Stuttgart\
💼 [LinkedIn](https://www.linkedin.com/in/eric-rosenberg-441649288/)\
🎮 [Instagram Devlog --
@IsorTowerDev](https://www.instagram.com/isor_gamedev)\
📧 Contact: *\[IsorDev@email.de\]*

------------------------------------------------------------------------

**© 2025 Eric Rosenberg -- Structured battles, clean systems, and chaos
inside the console.**
