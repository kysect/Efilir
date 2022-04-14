using System;
using System.Diagnostics;

namespace Efilir.Core.Types
{
    [DebuggerDisplay("{_angle}")]
    public readonly struct Angle
    {
        public static Angle Zero = FromDegree(0);

        private readonly double _angle;

        public double AngleInRadians => _angle;
        public double AngleInDegree => _angle * 180 / Math.PI;

        public static Angle FromDegree(double degree) => new Angle(degree / 180 * Math.PI);
        public static Angle FromRadian(double radian) => new Angle(radian);

        private Angle(double angle)
        {
            _angle = angle;
        }

        public static bool operator >(Angle a, Angle b) => a._angle > b._angle;
        public static bool operator <(Angle a, Angle b) => a._angle < b._angle;
        public static Angle operator *(Angle a, double b) => new Angle(a._angle * b);

        public Angle Abs()
        {
            return new Angle(Math.Abs(_angle));
        }
    }
}