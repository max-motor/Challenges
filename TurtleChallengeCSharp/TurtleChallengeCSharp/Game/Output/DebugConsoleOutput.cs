using System;

namespace TurtleChallengeCSharp.Game.Output
{
    public class DebugConsoleOutput : ConsoleOutput
    {
        public override void Rotated(Direction direction)
        {
            Console.WriteLine(direction);
            Console.ReadKey();
        }

        public override void Moved(Coordinates position)
        {
            Console.WriteLine(position);
            Console.ReadKey();
        }

        public override void HitMine()
        {
            base.HitMine();
            Console.ReadKey();
        }

        public override void Error(string message)
        {
            base.Error(message);
            Console.ReadKey();
        }

        public override void Won()
        {
            base.Won();
            Console.ReadKey();
        }
        public override void DidNotWin()
        {
            base.DidNotWin();
            Console.ReadKey();
        }
    }
}