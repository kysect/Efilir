using System;
using System.Collections.Generic;

namespace Efilir.Core.Types
{
    public record Coordinate(int X, int Y)
    {
        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public double Distance(Coordinate other)
        {
            return (this - other).Length();
        }

        public Coordinate Abs()
        {
            return new Coordinate(Math.Abs(X), Math.Abs(Y));
        }

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

        public static Coordinate operator +(Coordinate left, Vector right)
        {
            return left + right.ToCoordinate();
        }

        public static Coordinate operator -(Coordinate left, Coordinate right)
        {
            int newX = left.X - right.X;
            int newY = left.Y - right.Y;
            return new Coordinate(newX, newY);
        }
    }
}