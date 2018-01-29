using System.Collections.Generic;
using NUnit.Framework;

namespace Task2_SpaceBombs
{
    [TestFixture]
    public class SpaceBombsSolutionTests
    {
        [TestCaseSource(typeof(MyFactoryClass), nameof(MyFactoryClass.AddSource))]
        public int FindSquareOfSafectDistance(List<BombCoordinates> bombs)
        {
            // Arrange
            var solution = new SpaceBombsSolution();

            // Act
            var result = solution.Run(bombs);

            // Assert
            return result;
        }

        public class MyFactoryClass
        {
            private const int SpaceSize  = 1000;

            public static IEnumerable<TestCaseData> AddSource
            {
                get
                {
                    yield return new TestCaseData(new List<BombCoordinates> { new BombCoordinates(0, 0, 0) }).Returns(SpaceSize * SpaceSize * 3);
                    yield return new TestCaseData(new List<BombCoordinates> { new BombCoordinates(0, 0, SpaceSize) }).Returns(SpaceSize * SpaceSize * 3);
                    yield return new TestCaseData(new List<BombCoordinates> { new BombCoordinates(0, SpaceSize, 0) }).Returns(SpaceSize * SpaceSize * 3);
                    yield return new TestCaseData(new List<BombCoordinates> { new BombCoordinates(0, SpaceSize, SpaceSize) }).Returns(SpaceSize * SpaceSize * 3);
                    yield return new TestCaseData(new List<BombCoordinates> { new BombCoordinates(SpaceSize, 0, 0) }).Returns(SpaceSize * SpaceSize * 3);
                    yield return new TestCaseData(new List<BombCoordinates> { new BombCoordinates(SpaceSize, 0, SpaceSize) }).Returns(SpaceSize * SpaceSize * 3);
                    yield return new TestCaseData(new List<BombCoordinates> { new BombCoordinates(SpaceSize, SpaceSize, 0) }).Returns(SpaceSize * SpaceSize * 3);
                    yield return new TestCaseData(new List<BombCoordinates> { new BombCoordinates(SpaceSize, SpaceSize, SpaceSize) }).Returns(SpaceSize * SpaceSize * 3);
                    yield return new TestCaseData(new List<BombCoordinates> { new BombCoordinates(0, 0, 0), new BombCoordinates(0, 0, SpaceSize), new BombCoordinates(0, SpaceSize, 0), new BombCoordinates(0, SpaceSize, SpaceSize), new BombCoordinates(SpaceSize, 0, 0), new BombCoordinates(SpaceSize, 0, SpaceSize), new BombCoordinates(SpaceSize, SpaceSize, 0), new BombCoordinates(SpaceSize, SpaceSize, SpaceSize), new BombCoordinates(SpaceSize, SpaceSize, SpaceSize) }).Returns(SpaceSize * SpaceSize * 3 / 4);
                }
            }
        }
    }

}
