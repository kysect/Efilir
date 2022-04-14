using System;

namespace Efilir.Core.Types
{
    public class AngleRotation
    {
        private int _rotate;
        private bool _inversedX;
        private bool _inversedY;

        public AngleRotation(int rotate)
        {
            Rotate = rotate;
            _inversedX = false;
            _inversedY = false;
        }

        public int Rotate
        {
            get => _rotate;
            set
            {
                while (value < 0)
                    value += 8;

                _rotate = value % 8;
            }
        }

        public Coordinate GetRotation()
        {
            (int x, int y) = GetRotationWithoutInverse();

            if (_inversedY)
                y *= -1;
            if (_inversedX)
                x *= -1;

            return new Coordinate(x, y);
        }

        public void InverseX() => _inversedX = !_inversedX;
        public void InverseY() => _inversedY = !_inversedY;

        public Coordinate GetRotationWithoutInverse()
        {
            switch (_rotate)
            {
                case 0:
                    return new Coordinate(0, -1);
                case 1:
                    return new Coordinate(1, -1);
                case 2:
                    return new Coordinate(1, 0);
                case 3:
                    return new Coordinate(1, 1);
                case 4:
                    return new Coordinate(0, 1);
                case 5:
                    return new Coordinate(-1, 1);
                case 6:
                    return new Coordinate(-1, 0);
                case 7:
                    return new Coordinate(-1, -1);
                default:
                    throw new ArgumentException();
            }
        }

        public static AngleRotation operator +(AngleRotation left, int right)
        {
            return new AngleRotation(left.Rotate + right);
        }

        public static AngleRotation operator +(AngleRotation left, AngleRotation right)
        {
            return new AngleRotation(left.Rotate + right.Rotate);
        }
    }
}