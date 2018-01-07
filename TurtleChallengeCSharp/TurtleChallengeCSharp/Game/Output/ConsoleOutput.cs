using System;

namespace TurtleChallengeCSharp.Game.Output
{
    public class ConsoleOutput : IOutput
    {
        public virtual void Rotated(Direction direction)
        {
        }

        public virtual void Moved(Coordinates position)
        {

        }

        public virtual void HitMine()
        {
            Console.WriteLine("Mine hit!");
        }

        public virtual void Error(string message)
        {
            Console.WriteLine(message);
        }

        public virtual void Won()
        {
            Console.WriteLine("Success!");
        }
        public virtual void DidNotWin()
        {
            Console.WriteLine("Still in danger!");
        }

        public void Start(int sequenceNumber)
        {
            Console.Write($"Sequence {sequenceNumber}: ");
        }
    }
}