using System.Collections.Generic;
using System.Linq;

namespace TurtleChallengeCSharp.Game
{
    public class Settings
    {
        public Coordinates StartPosition { get; set; }
        public Direction StartDirection { get; set; }
        public Coordinates Exit { get; set; }
        public List<Coordinates> Mines { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public bool IsValid()
        {
            return
                Width > 0
                && Height > 0
                && IsValidPosition(StartPosition)
                && IsValidPosition(Exit)
                && !StartPosition.IsEqual(Exit)
                && !Mines.Any(x => x.IsEqual(StartPosition))
                && !Mines.Any(x => x.IsEqual(Exit));
        }

        private bool IsValidPosition(Coordinates position)
        {
            return position.X >= 0
                   && position.X < Width
                   && position.Y >= 0
                   && position.Y < Height;
        }
    }
}