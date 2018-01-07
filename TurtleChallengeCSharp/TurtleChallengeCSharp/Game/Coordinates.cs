namespace TurtleChallengeCSharp.Game
{
    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsEqual(Coordinates other)
        {
            return other != null && other.X == X && other.Y == Y;
        }

        public override string ToString()
        {
            return $"[X:{X}, Y:{Y}]";
        }
    }
}