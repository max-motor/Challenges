using System.Collections.Generic;
using NUnit.Framework;

namespace Task3_Recursion
{
    [TestFixture]
    public class RecursiveAdditionSolutionTests
    {
        [TestCaseSource(typeof(MyFactoryClass), nameof(MyFactoryClass.AddSource))]
        public byte[] Add_UsingARecursiveAlgorithm_ValuesAreAdded(byte[] f, byte[] s)
        {
            // Arrange
            var solution = new RecursiveAdditionSolution();

            // Act
            var result = solution.AddRecursive(f, s);

            // Assert
            return result;
        }

        public class MyFactoryClass
        {
            public static IEnumerable<TestCaseData> AddSource
            {
                get
                {
                    yield return new TestCaseData(new byte[] { }, new byte[] { }).Returns(new byte[] { });
                    yield return new TestCaseData(new byte[] { 1, 1, 1 }, new byte[] { 1, 1, 1 }).Returns(new byte[] { 2, 2, 2 });
                    yield return new TestCaseData(new byte[] { 1, 1, 255 }, new byte[] { 0, 0, 1 }).Returns(new byte[] { 1, 2, 0 });
                    yield return new TestCaseData(new byte[] { 255, 255, 255 }, new byte[] { 0, 0, 1 }).Returns(new byte[] { 0, 0, 0 });
                    yield return new TestCaseData(new byte[] { 0, 255, 255 }, new byte[] { 0, 1, 2 }).Returns(new byte[] { 1, 1, 1 });
                    yield return new TestCaseData(CreateLargeArray(LargeArraySize, 1), CreateLargeArray(LargeArraySize, 2)).Returns(CreateLargeArray(LargeArraySize, 3));
                }
            }

            const int LargeArraySize = 10000;

            static byte[] CreateLargeArray(int length, byte value)
            {
                var result = new byte[length];
                for (int i = 0; i < length; i++)
                {
                    result[i] = value;
                }

                return result;
            }
        }
    }
}
