using System.Collections.Generic;

namespace Task2_SpaceBombs
{
    public class SpaceBombsSolution
    {
        private const int SpaceSize = 1000;
        const int MaxSafeDistance = SpaceSize * SpaceSize * 3;

        private static int Square(int x)
        {
            return x*x;
        }

        /// <summary>
        /// Calculates minimum distance for all bombs
        /// </summary>
        /// <param name="bombs">Coordinates of bombs</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        /// <param name="currentKnowSafe">Previously calculated safe distance</param>
        /// <returns></returns>
        private static int MinDistance(IEnumerable<BombCoordinates> bombs, int x, int y, int z, int currentKnowSafe)
        {
            var minDistance = MaxSafeDistance;

            foreach (var bomb in bombs)
            {
                // calculating square of the distance between current bomb and point passed as parameter
                var currentDistance = Square(x - bomb.X) + Square(y - bomb.Y) + Square(z - bomb.Z);

                // Optimisation for previously calculated safe distance
                if (currentDistance <= currentKnowSafe)
                    return currentKnowSafe;

                // take minimum distance
                if (currentDistance < minDistance)
                    minDistance = currentDistance;
            }

            return minDistance;
        }

        public int Run(List<BombCoordinates> bombs)
        {
            // setting default distance 
            int bestSafe = 0;
            
            // go over each point in the (0, 0, 0) .. (1000, 1000, 1000) space
            for (var x = 0; x <= SpaceSize; x++)
            {
                for (var y = 0; y <= SpaceSize; y++)
                {
                    for (var z = 0; z <= SpaceSize; z++)
                    {
                        int currectSafe = MinDistance(bombs, x, y, z, bestSafe);

                        //Take maximum safe distance
                        if (currectSafe > bestSafe)
                            bestSafe = currectSafe;
                    }
                }
            }

            return bestSafe;
        }
    }
}
