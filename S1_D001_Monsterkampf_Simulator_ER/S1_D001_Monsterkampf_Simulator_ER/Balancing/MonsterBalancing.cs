/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : MonsterBalancing.cs
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
* Provides all balancing base values and level scaling rules for all monsters.
*
* History :
* xx.xx.2025 ER Created
******************************************************************************/

using S1_D001_Monsterkampf_Simulator_ER.Managers;
using S1_D001_Monsterkampf_Simulator_ER.Monsters;

namespace S1_D001_Monsterkampf_Simulator_ER.Balancing
{
    internal class MonsterBalancing
    {
        // === Dependencies ===
        private readonly DiagnosticsManager _diagnostics;

        // === Fields ===

        // Dictionaries für Basiswerte
        private readonly Dictionary<RaceType, BaseStats> _baseStats;

        private readonly Dictionary<RaceType, BaseResistances> _baseResistances;

        // Skalierungsfaktoren
        // --- Stat Point Scaling for Level up---
        public int StartLevel => 1;

        public int LevelUpScaling => 1;
        public int BonusLevels => 2;
        public int BaseVictoryReward => 1;
        public float HPScaling => 0.10f;
        public float APScaling => 0.04f;
        public float DPScaling => 0.05f;
        public float SpeedScaling => 0.08f;

        // --- Stat Point deffinition Scaling  ---
        public int StatIncrease_HP => 10;

        public int StatIncrease_AP => 1;
        public int StatIncrease_DP => 1;
        public float StatIncrease_Speed => 0.5f;

        public MonsterBalancing(DiagnosticsManager diagnostics)
        {
            _diagnostics = diagnostics;
            _baseStats = new Dictionary<RaceType, BaseStats>();
            _baseResistances = new Dictionary<RaceType, BaseResistances>();

            _baseStats[RaceType.Goblin] = new BaseStats(HP: 48, AP: 17, DP: 2, Speed: 14);
            _baseStats[RaceType.Slime] = new BaseStats(HP: 80, AP: 10, DP: 4, Speed: 6);
            _baseStats[RaceType.Troll] = new BaseStats(HP: 70, AP: 15, DP: 3, Speed: 8);
            _baseStats[RaceType.Orc] = new BaseStats(HP: 55, AP: 12, DP: 4, Speed: 10);

            _baseResistances[RaceType.Goblin] = new BaseResistances(Physical: 0.00f, Fire: 0.00f, Water: 0.00f, Poison: 0.20f);
            _baseResistances[RaceType.Slime] = new BaseResistances(Physical: 0.10f, Fire: -0.10f, Water: 0.30f, Poison: 0.10f);
            _baseResistances[RaceType.Troll] = new BaseResistances(Physical: 0.05f, Fire: 0.00f, Water: 0.00f, Poison: -0.20f);
            _baseResistances[RaceType.Orc] = new BaseResistances(Physical: 0.15f, Fire: -0.10f, Water: 0.00f, Poison: 0.00f);
        }

        public MonsterMeta GetMeta(RaceType race, int level)
        {
            if (!_baseStats.TryGetValue(race, out BaseStats stats))
            {
                throw new Exception($"No BaseStats found for race: {race}.");
            }

            if (level < 1)
            {
                throw new ArgumentException("level must be at least 1.");
            }

            int scale = level - 1;
            float scaledHp = stats.HP * (1 + HPScaling * scale);
            float scaledAp = stats.AP * (1 + APScaling * scale);
            float scaledDp = stats.DP + (DPScaling * scale);
            float scaledSpeed = stats.Speed + (SpeedScaling * scale);

            _diagnostics.AddCheck($"{nameof(MonsterBalancing)}.{nameof(GetMeta)}: Successfully got balanced meta data.");

            return new MonsterMeta(scaledHp, scaledHp, scaledAp, scaledDp, scaledSpeed);
        }

        public MonsterResistance GetResistance(RaceType race)
        {
            if (!_baseResistances.TryGetValue(race, out BaseResistances resistances))
            {
                throw new Exception($"No BaseResistances found for race: {race}.");
            }
            _diagnostics.AddCheck($"{nameof(MonsterBalancing)}.{nameof(GetResistance)}: Successfully got balanced resistance data.");

            return new MonsterResistance(physical: resistances.Physical, fire: resistances.Fire, water: resistances.Water, poison: resistances.Poison);
        }

        private record struct BaseStats(int HP, int AP, int DP, int Speed);
        private record struct BaseResistances(float Physical, float Fire, float Water, float Poison);
    }
}