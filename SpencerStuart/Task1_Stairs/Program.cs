using System;
using Newtonsoft.Json;

namespace Task1_Stairs
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read task settings from a json file
            string fileName;
            if (args.Length == 0)
            {
                Console.WriteLine("Input file not passed as a parameter, loading settings from Task1Input.json");
                fileName = "Task1Input.json";
            }
            else
            {
                fileName = args[0];
            }

            //Deserialize settings from json
            var settings = JsonConvert.DeserializeObject<StairsTaskSettings[]>(System.IO.File.ReadAllText(fileName));

            // Run solution for each case
            var solution = new StairsTaskSolution();
            for (var i = 0; i < settings.Length; i++)
            {
                var setting = settings[i];
                var result = solution.Run(setting.Flights, setting.StepsPerStride);
                Console.WriteLine(
                    $"Case: {i + 1}, Flights: {{{string.Join(", ", setting.Flights)}}}, StepsPerStride: {setting.StepsPerStride}, Result: {result}");
            }

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
