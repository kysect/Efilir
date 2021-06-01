using System;
using Efilir.Core.Cells;
using Efilir.Core.Generics.Environment;
using Efilir.Core.Tools;
using Efilir.Core.Types;

namespace Efilir.Core.PredefinedCells.Cells
{
    public class PredefinedCell : IBaseCell
    {
        public Coordinate Position { get; set; }
        public Vector RealPosition { get; private set; }
        public AngleRotation CurrentRotate { get; set; }
        public PredefinedCellType CellType { get; }

        private PredefinedCellGameArea _gameArea;
        private Vector _velocityDirection;

        public PredefinedCell(PredefinedCellGameArea gameArea, Vector position, Vector velocity, PredefinedCellType cellType)
        {
            _gameArea = gameArea;
            RealPosition = position;
            Position = RealPosition.ToCoordinate();
            _velocityDirection = velocity;
            CellType = cellType;
        }

        public void MakeTurn(IGenericGameArea gameArea)
        {
            double timeDelta = 0.1;
            Vector newAcceleration = CalculateNewAccelerationWithoutDistance();
            Vector newVelocity = _velocityDirection + newAcceleration * timeDelta;
            var newPosition = RealPosition + newVelocity * timeDelta;
            RealPosition = RoundPosition(newPosition);
            Position = RealPosition.ToCoordinate();

            var wallType = _gameArea.GetWallType(newPosition);
            if (wallType != WallType.Undefined)
            {
                double pushCoefficient = -1;
                if (wallType.HasFlag(WallType.Left) && newVelocity.X < 0 
                    || wallType.HasFlag(WallType.Right) && newVelocity.X > 0)
                {
                    newVelocity = newVelocity with { X = newVelocity.X * pushCoefficient };
                }

                if (wallType.HasFlag(WallType.Bottom) && newVelocity.Y < 0
                    || wallType.HasFlag(WallType.Top) && newVelocity.Y > 0)
                {
                    newVelocity = newVelocity with { Y = newVelocity.Y * pushCoefficient };
                }
            }

            _velocityDirection = newVelocity;
        }

        public Vector CalculateNewAccelerationWithoutDistance()
        {
            Vector newDirection = new Vector(0, 0);

            foreach ((var type, Vector predefinedCellPosition) in _gameArea.PreviousCellPosition)
            {
                Vector moveDirection = predefinedCellPosition - RealPosition;
                if (moveDirection.Length() > 50 || moveDirection.Length() > Double.Epsilon)
                    continue;

                if (!IsCellOnWay(moveDirection))
                    continue;

                if (type == CellType)
                    newDirection += moveDirection / moveDirection.Length();
                //else
                //    newDirection -= moveDirection / moveDirection.Length();
            }

            if (newDirection.Length() < Double.Epsilon)
                return newDirection;

            return newDirection / 2;
        }

        public bool IsCellOnWay(Vector moveDirection)
        {
            double visibleAngle = 10.0 / 180;

            double velocityAngle = Math.Atan(_velocityDirection.Y / _velocityDirection.X);
            double toObjectAngle = Math.Atan(moveDirection.Y / moveDirection.X);

            var maxAngle = Math.Max(velocityAngle, toObjectAngle);
            var minAngle = Math.Min(velocityAngle, toObjectAngle);
            return (maxAngle - minAngle < visibleAngle || (360 - (maxAngle - minAngle) < visibleAngle));
        }

        private double CalcMoveVector(double distance)
        {
            if (Math.Abs(distance) < 0.5)
                return 0;
            return 1.0 / distance / Math.Abs(distance);
        }

        private Vector RoundPosition(Vector position)
        {
            double x = position.X;
            if (x < 0)
                x = 0;
            if (x >= _gameArea.AreaSize)
                x = _gameArea.AreaSize - 1;

            double y = position.Y;
            if (y < 0)
                y = 0;
            if (y >= _gameArea.AreaSize)
                y = _gameArea.AreaSize - 1;

            return new Vector(x, y);
        }

        public override string ToString()
        {
            return $"PredefinedCell on {Position} with angle {CurrentRotate} age";
        }
    }
}