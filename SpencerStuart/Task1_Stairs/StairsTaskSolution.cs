using System;
using System.Linq;

namespace Task1_Stairs
{
    public class StairsTaskSolution
    {
        /// <summary>
        /// Executes the solution to "Stairs" problem
        /// </summary>
        /// <param name="flights">Flights of stairs</param>
        /// <param name="stepsPreStride">S</param>
        /// <returns></returns>
        public int Run(int[] flights, int stepsPreStride)
        {
            if (flights == null || flights.Length == 0 || flights.Length > 50)
            {
                throw new ArgumentException("Staircase must contain between 1 and 50 flights of stairs");
            }

            if (flights.Any(x => x < 5 || x > 30))
            {
                throw new ArgumentException("Each flight of stairs must be between 5 and 30 steps, inclusive");
            }

            if (stepsPreStride < 2 || stepsPreStride > 5)
            {
                throw new ArgumentException("StepsPerStride must be between 2 and 5, inclusive");
            }

            int steps = 0;

            // Calculate number of steps per flight rounding up to stride size
            Array.ForEach(flights, f => steps += (int)Math.Ceiling((double)f / stepsPreStride));

            //Add number of landings multiplied by 2 steps per landing
            steps += 2 * (flights.Length - 1);

            return steps;
        }
    }
}
