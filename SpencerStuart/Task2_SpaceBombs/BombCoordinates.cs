namespace Task2_SpaceBombs
{
    public class BombCoordinates
    {
        public BombCoordinates(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public bool Equals(int x, int y, int z)
        {
            return x == X && y == Y && z == Z;
        }
    }
}
