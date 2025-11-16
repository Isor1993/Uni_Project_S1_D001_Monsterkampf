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

using S1_D001_Monsterkampf_Simulator_ER.Monsters;
using S1_D001_Monsterkampf_Simulator_ER.Skills.Slime;

namespace S1_D001_Monsterkampf_Simulator_ER.Balancing
{
    internal class MonsterBalancing
    {

        // === Dependencies ===

        // === Fields ===

        // Dictionaries für Basiswerte
        private readonly Dictionary<RaceType, BaseStats> _baseStats;
        private readonly Dictionary<RaceType, BaseResistances> _baseResistances;


        // Skalierungsfaktoren
        private const float HPScaling = 0.10f;
        private const float APScaling = 0.04f;
        private const float DPScaling = 0.5f;
        private const float SpeedScaling = 0.3f;

        public MonsterBalancing()
        {
            _baseStats = new Dictionary<RaceType, BaseStats>();
            _baseResistances = new Dictionary<RaceType, BaseResistances>();

            _baseStats[RaceType.Goblin] = new BaseStats(HP: 40, AP: 10, DP: 2, Speed: 14);
            _baseStats[RaceType.Slime] = new BaseStats(HP: 70, AP: 8, DP: 4, Speed: 6);
            _baseStats[RaceType.Troll] = new BaseStats(HP: 60, AP: 15, DP: 3, Speed: 8);
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

            

            return new MonsterMeta(scaledHp,scaledHp,scaledAp,scaledDp,scaledSpeed);
           
        }

        public MonsterResistance GetResistance(RaceType race)
        {
            if(!_baseResistances.TryGetValue(race,out BaseResistances resistances))
            {
                throw new Exception($"No BaseResistances found for race: {race}.");
            }
            
            return new MonsterResistance(physical:resistances.Physical,fire:resistances.Fire,water:resistances.Water,poison:resistances.Poison);
        }
   
        private record struct BaseStats(int HP, int AP, int DP, int Speed);
        private record struct BaseResistances(float Physical, float Fire, float Water, float Poison);
    }
}