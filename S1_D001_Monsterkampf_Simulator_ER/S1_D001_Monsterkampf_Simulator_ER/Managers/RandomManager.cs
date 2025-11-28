/*****************************************************************************
* Project : Monsterkampf-Simulator (K1, S1, S4)
* File    : RandomManager.cs
* Date    : 03.12.2025
* Author  : Eric Rosenberg
*
* Description :
*   Centralized random generator for the entire project.
*   Provides deterministic access to random values and utilities
*   such as picking enemies or generating random rolls.
*
* Responsibilities :
*   - Provide random number generation wrapped in one shared class
*   - Avoid multiple Random instances in the project
*   - Ensure reproducible behaviour if needed (easy to extend)
*   - Select random monster races while avoiding duplicates
*
* History :
*   03.12.2025 ER Created
******************************************************************************/


using S1_D001_Monsterkampf_Simulator_ER.Monsters;

namespace S1_D001_Monsterkampf_Simulator_ER.Managers
{
    internal class RandomManager
    {

        private readonly Random _random;

        /// <summary>
        /// Creates a new RandomManager and initializes
        /// the underlying Random instance.
        /// </summary>
        public RandomManager()
        {
            _random = new Random();
        }

        /// <summary>
        /// Returns a random integer from 0 (inclusive) up to the given
        /// <paramref name="maxExclusive"/> value (exclusive).
        /// </summary>
        /// <param name="maxExclusive">
        /// Upper bound for the random value (exclusive).
        /// </param>
        /// <returns>
        /// A random integer in the range [0 .. maxExclusive-1].
        /// </returns>
        public int Next(int maxExclusive)
        {

            return _random.Next(maxExclusive);
        }

        /// <summary>
        /// Picks a random monster race for an enemy, ensuring that
        /// it does NOT match the player's race.
        /// Useful for generating new opponents after each fight.
        /// </summary>
        /// <param name="racePlayer">
        /// The race of the player's monster. The returned race will always differ.
        /// </param>
        /// <returns>
        /// A random <see cref="RaceType"/> that is not equal to <paramref name="racePlayer"/>.
        /// </returns>
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
