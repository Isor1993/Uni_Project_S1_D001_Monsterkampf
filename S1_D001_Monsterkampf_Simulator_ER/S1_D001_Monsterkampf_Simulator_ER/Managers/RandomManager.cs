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

using S1_D001_Monsterkampf_Simulator_ER.Monsters;

namespace S1_D001_Monsterkampf_Simulator_ER.Managers
{
    internal class RandomManager
    {
        private readonly Random _random;

        public RandomManager()
        {
            _random = new Random();
        }

        // Zufallszahl von 0 (inklusive) bis maxExclusive (exklusiv)
        public int Next(int maxExclusive)
        {
            return _random.Next(maxExclusive);
        }

        // Optional: falls du später einen Bereich brauchst
        public int Next(int minInclusive, int maxExclusive)
        {
            return _random.Next(minInclusive, maxExclusive);
        }

        public RaceType PickRandomRace(RaceType racePlayer)
        {
            RaceType enemy;
            do
            {
                int rndroll = _random.Next(4);

                enemy = rndroll switch
                {
                    0 => RaceType.Goblin,
                    1 => RaceType.Troll,
                    2 => RaceType.Orc,
                    3 => RaceType.Slime,
                    _ => RaceType.None,
                };
            } while (enemy == racePlayer);

            return enemy;
        }
    }
}