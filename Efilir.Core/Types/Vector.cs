using System;

namespace Efilir.Core.Types
{
    public record Vector(double X, double Y)
    {
        public static Vector CreateFromAngle(Angle angle, double length)
        {
            return new Vector(Math.Sin(angle.AngleInRadians) * length, Math.Cos(angle.AngleInRadians) * length);
        }

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public double Distance(Vector other)
        {
            return (this - other).Length();
        }

        public Vector Abs()
        {
            return new Vector(Math.Abs(X), Math.Abs(Y));
        }

        public Vector RotateToAngle(Angle angle)
        {
            double newX = X * Math.Cos(angle.AngleInRadians) - Y * Math.Sin(angle.AngleInRadians);
            double newY = X * Math.Sin(angle.AngleInRadians) + Y * Math.Cos(angle.AngleInRadians);

            return new Vector(newX, newY);
        }

        public Angle AngleTo(Vector other)
        {
            double velocityAngle = Math.Atan(Y / X);
            double toObjectAngle = Math.Atan(other.Y / other.X);

            double delta = toObjectAngle - velocityAngle;
            if (delta > Math.PI)
                delta -= Math.PI * 2;

            return Angle.FromRadian(delta);
        }

        public Vector ResizeTo(double length)
        {
            return this * length / this.Length();
        }

        public static Vector operator +(Vector left, Vector right)
        {
            var newX = left.X + right.X;
            var newY = left.Y + right.Y;
            return new Vector(newX, newY);
        }

        public static Vector operator -(Vector left, Vector right)
        {
            var newX = left.X - right.X;
            var newY = left.Y - right.Y;
            return new Vector(newX, newY);
        }

        public static Vector operator *(Vector left, double coefficient)
        {
            return new Vector(left.X * coefficient, left.Y * coefficient);
        }

        public static Vector operator /(Vector left, double coefficient)
        {
            return new Vector(left.X / coefficient, left.Y / coefficient);
        }

        public Coordinate ToCoordinate()
        {
            return new Coordinate((int)X, (int)Y);
        }
    }
}