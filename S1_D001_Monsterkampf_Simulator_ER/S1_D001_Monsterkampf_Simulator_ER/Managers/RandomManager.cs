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
    }

}
}
