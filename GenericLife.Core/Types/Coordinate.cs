using System.Collections.Generic;

namespace GenericLife.Core.Types
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

        public IEnumerable<Coordinate> EnumerateAround(AngleRotation from)
        {
            for (var i = 0; i < 8; i++)
                yield return this + (from + i).GetRotation();
        }

        public static Coordinate operator +(Coordinate left, Coordinate right)
        {
            int newX = left.X + right.X;
            int newY = left.Y + right.Y;
            return new Coordinate(newX, newY);
        }

        public static bool operator ==(Coordinate left, Coordinate right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        public static bool operator !=(Coordinate left, Coordinate right)
        {
            return !(left == right);
        }
    }
}