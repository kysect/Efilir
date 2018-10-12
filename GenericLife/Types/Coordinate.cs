namespace GenericLife.Types
{
    public class Coordinate
    {
        public Coordinate(int x, int y)
        {
            Y = y;
            X = x;
        }

        public int X { get; }
        public int Y { get; }

        public static Coordinate operator +(Coordinate left, Coordinate right)
        {
            var newX = left.X + right.X;
            var newY = left.Y + right.Y;
            return new Coordinate(newX, newY);
        }
    }
}