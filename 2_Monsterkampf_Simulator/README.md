# 📘 README

### SAE Institute Stuttgart

**Modul:** D001 -- Game Programming Basics (K1 / S1 / S4)\
**Student:** Eric Rosenberg\
**Projekt:** Monsterkampf-Simulator (Console Edition)

------------------------------------------------------------------------

## 1. Basis-Modul

Dies ist die Abgabe von **Eric Rosenberg** für das Modul D001 -- Game
Programming Basics (K1 / S1 / S4) am SAE Institute Stuttgart.

Das Projekt **„Monsterkampf-Simulator"** wurde in **C# (.NET 8.0)** als
Konsolenanwendung entwickelt.\
Es handelt sich um ein rundenbasiertes **1-gegen-1-Kampfsystem** mit
modularer Architektur, Damage-Pipeline, Status-Effekten und
erweiterbarem Skill-/Monsterframework.

------------------------------------------------------------------------

## 2. Abgabe nicht vorhanden

*(nicht zutreffend -- alle geforderten Bestandteile vorhanden)*

------------------------------------------------------------------------

## 3. Mehrere Abgaben in einem Ordner

*(nicht zutreffend -- Einzelprojekt)*

------------------------------------------------------------------------

## 4. Gruppenarbeit

*(nicht zutreffend -- Einzelarbeit von Eric Rosenberg)*

------------------------------------------------------------------------

## 5. Feature-Beschreibung

### 🧩 Hauptfunktionen & Systemübersicht

Der **Monsterkampf-Simulator** beinhaltet ein vollständiges
rundenbasiertes Kampfsystem:

-   vier unterschiedliche Monster (Goblin, Orc, Troll, Slime)\
-   aktive und passive Skills\
-   Buffs, Debuffs, DOT, Regeneration\
-   Damage Pipeline (mehrstufige Schadensberechnung)\
-   Cooldowns & Rundenlogik\
-   KI-gesteuerter Gegner\
-   ASCII-Sprites für beide Seiten\
-   vollständige Manager-Architektur (SRP)

------------------------------------------------------------------------

### 🎮 Kernsysteme

-   **Monsterwahl & Spawn-Effekte**\
-   **Skill-Menü & Spielerentscheidungen**\
-   **DamagePipeline**: Raw Damage → Resistenz → Status → Final\
-   **Status Effects System**: Poison, Fear, Absorb, Regeneration, Tribe
    Scream Buff\
-   **Cooldown-System**\
-   **Turn-Based Flow**
    -   Start-of-Turn Effekte\
    -   Player Action\
    -   Enemy Action\
    -   DOTs, Durations, Cooldowns\
-   **ASCII UI** (Player Box, Enemy Box, Message Box, Skill Fenster)

------------------------------------------------------------------------

### 🧠 Manager-Architektur

-   **GameManager** -- Hauptfluss des Spiels\
-   **BattleManager** -- Rundenabläufe, Angriffe, Skillauswahl\
-   **UIManager** -- Rendering aller UI-Elemente\
-   **ScreenManager** -- Start/Tutorial/Fight/Result Screens\
-   **InputManager** -- einheitliche Eingabe-Schnittstelle\
-   **PlayerController** -- Spielerlogik\
-   **EnemyController** -- KI basierend auf Skills & Cooldowns\
-   **DamagePipeline** -- zentrale Schadensberechnung\
-   **MonsterFactory** -- Monster & Skill-Erstellung\
-   **RandomManager** -- Zufallsentscheidungen\
-   **DiagnosticsManager** -- Logging (Fehler, Warnungen, Checks)\
-   **PrintManager** -- Konsolen-Rendering\
-   **SymbolManager** -- ASCII-Symbole

------------------------------------------------------------------------

### ⚙️ Technische Eckdaten

-   **Sprache:** C#\
-   **Framework:** .NET 8.0\
-   **IDE:** Visual Studio 2022\
-   **Plattform:** Windows Console\
-   **Architektur:** Modular, SRP, klare Verantwortlichkeiten\
-   **Dokumentation:** XML-Kommentare + Debug-Logging

------------------------------------------------------------------------

## 📂 Ordnerstruktur (korrekt angepasst)

    Monsterkampf-Simulator/
    │
    ├── src/                        # Vollständiger Sourcecode
    │   ├── Monsters/
    │   ├── Skills/
    │   ├── Systems/
    │   ├── Managers/
    │   ├── Dependencies/
    │   ├── Factories/
    │   └── Program.cs
    │
    ├── release/                    # Kompilierte .exe Dateien
    │
    └── other/                      # Zusätzliche Abgabedateien
        ├── Dokumente/
        │      ├── doku.pdf
        │      └── Extras and Old/
        │
        ├── Screenshots/
        │      ├── Screenshot_01.png
        │      ├── Screenshot_02.png
        │      ├── ...
        │
        └── Gameplay.mp4

------------------------------------------------------------------------

## 🧾 Abgabebeschreibung (nach SAE-Vorgabe)

-   **Art der Abgabe:** Einzelarbeit\
-   **Medien:**
    -   1 Gameplay-Video (30--90 Sekunden)\
    -   min. 3 Screenshots\
    -   doku.pdf im Ordner „Dokumente"\
-   **Pflichtdatei:** README.md\
-   **Vorgaben eingehalten:** Ja

------------------------------------------------------------------------

## 🧠 Zusammenfassung

Der **Monsterkampf-Simulator** demonstriert ein komplettes
Turn-Based-Kampfsystem mit Damage Pipeline, Status Effects,
aktiven/passiven Skills und modularer Architektur.\
Das Projekt ist leicht erweiterbar (neue Monster, Skills, Effekte) und
bildet eine ideale Grundlage für RPG- oder Roguelike-Systeme.

------------------------------------------------------------------------

**Stuttgart, 03. Dezember 2025**\
*© 2025 Eric Rosenberg -- SAE Institute Stuttgart*
