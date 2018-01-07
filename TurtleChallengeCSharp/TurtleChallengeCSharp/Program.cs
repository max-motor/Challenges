using System;
using System.IO;
using TurtleChallengeCSharp.Game;

namespace TurtleChallengeCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
                throw new ArgumentException("Game should have at 2 parameters: <settings> file and <moves> files.\nRun: TurtleChallengeCSharp.EXE <SettingsFileName> <MovesFileName>");

            if (!File.Exists(args[0]))
                throw new FileNotFoundException("Settings file not found: " + args[0]);

            if (!File.Exists(args[1]))
                throw new FileNotFoundException("Moves file not found: " + args[1]);

            var settingsJson = File.ReadAllText(args[0]);
            var movesJson = File.ReadAllText(args[1]);

            var game = new GameChallenge();
            game.Play(settingsJson, movesJson);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
