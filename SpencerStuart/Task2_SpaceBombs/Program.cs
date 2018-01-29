using System;
using System.Collections.Generic;
using System.IO;

namespace Task2_SpaceBombs
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read task settings from a text file
            string fileName;
            if (args.Length == 0)
            {
                fileName = "Input.txt";
                Console.WriteLine($"Input file not passed as a parameter, loading settings from {fileName}");
            }
            else
            {
                fileName = args[0];
            }

            //Load cases
            var cases = LoadCases(fileName);

            // Run solution for each case
            var solution = new SpaceBombsSolution();
            for (var i = 0; i < cases.Count; i++)
            {
                var result = solution.Run(cases[i]);
                Console.WriteLine($"Case: {i + 1}, Result (square of distance): {result}");
            }

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private static List<List<BombCoordinates>> LoadCases(string fileName)
        {
            var testCases = new List<List<BombCoordinates>>();

            // Assuming the input is "clean"
            using (var file = new StreamReader(fileName))
            {
                // not using it, but following tasks's file format description
                var numCases = file.ReadLine();

                string line;
                while ((line = file.ReadLine()) != null)
                    testCases.Add(ParseLine(line));
            }

            return testCases;
        }

        private static List<BombCoordinates> ParseLine(string testCaseString)
        {
            var testCase = new List<BombCoordinates>();
            var bombCoordinatesSplit = testCaseString.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            int numBombs = int.Parse(bombCoordinatesSplit[0]);
            for (var i = 1; i <= numBombs; i++)
            {
                var bombIdx = (i - 1) * 3;
                testCase.Add(new BombCoordinates(
                 /* X */   int.Parse(bombCoordinatesSplit[bombIdx + 1]),
                 /* Y */   int.Parse(bombCoordinatesSplit[bombIdx + 2]),
                 /* Z */   int.Parse(bombCoordinatesSplit[bombIdx + 3])
                ));
            }

            return testCase;
        }
    }
}
