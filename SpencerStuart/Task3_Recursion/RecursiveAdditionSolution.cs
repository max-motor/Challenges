using System;

namespace Task3_Recursion
{
    class RecursiveAdditionSolution
    {
        public byte[] AddRecursive(byte[] first, byte[] second)
        {
            // Not checking for null as per task conditions
            var result = new byte[first.Length];

            return AddRecursive(first, second, result, first.Length - 1, 0); ;
        }

        public byte[] AddRecursive(byte[] first, byte[] second, byte[] result, int currentIndex, byte carriedOver)
        {
            // Exit condition
            if (currentIndex == -1)
            {
                return result;
            }

            // Calculating
            var currentValue = first[currentIndex] + second[currentIndex] + carriedOver;

            // Integer division equivalent to Math.Floor(currentValue/255) to check of we need to carry over 1
            carriedOver = (byte)(currentValue / 255);

            // Take into account carried over value to stay within byte range
            result[currentIndex] = (byte) (currentValue - carriedOver*256);

            return AddRecursive(first, second, result, currentIndex - 1, carriedOver);
        }
    }
}
