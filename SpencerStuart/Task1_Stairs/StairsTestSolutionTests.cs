using System;
using NUnit.Framework;

namespace Task1_Stairs
{
    [TestFixture]
    public class StairsTestSolutionTests
    {
        [TestCase(new[] { 15 }, 2, 8)]
        [TestCase(new[] { 15, 15 }, 2, 18)]
        [TestCase(new[] { 5, 11, 9, 13, 8, 30, 14 }, 3, 44)]
        public void TestSolution(int[] flights, int stepsPerStride, int expectedSteps)
        {
            // Setup
            var solution = new StairsTaskSolution();

            //Run
            var result = solution.Run(flights, stepsPerStride);

            //Assert
            Assert.AreEqual(expectedSteps, result);
        }

        [TestCase(0)]
        [TestCase(51)]
        public void SolutionThrowsArgumentExceptionWhenFlightsAreOutOfRange(int numberOfFligts)
        {
            // Setup
            var flights = new int[numberOfFligts];
            var solution = new StairsTaskSolution();

            //Run, expect ArgumentException
            Assert.Throws<ArgumentException>(()=> solution.Run(flights, 2), "Staircase must contain between 1 and 50 flights of stairs") ;
        }

        [TestCase(4)]
        [TestCase(31)]
        public void SolutionThrowsArgumentExceptionWhenNumberOfStepsIsOutOfRange(int numberOfSteps)
        {
            // Setup
            var flights = new [] { numberOfSteps };
            var solution = new StairsTaskSolution();

            //Run, expect ArgumentException
            Assert.Throws<ArgumentException>(() => solution.Run(flights, 2), "Each flight of stairs must be between 5 and 30 steps, inclusive");
        }

        [TestCase(1)]
        [TestCase(6)]
        public void SolutionThrowsArgumentExceptionWhenStepsPerStrideIsOutOfRange(int stepsPerStride)
        {
            // Setup
            var solution = new StairsTaskSolution();

            //Run, expect ArgumentException
            Assert.Throws<ArgumentException>(() => solution.Run(new[] { 10 }, stepsPerStride), "StepsPerStride must be between 2 and 5, inclusive");
        }
    }
}
