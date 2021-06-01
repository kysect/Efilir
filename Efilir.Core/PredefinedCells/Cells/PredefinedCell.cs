using System;
using System.Linq;
using Efilir.Core.Cells;
using Efilir.Core.Generics.Environment;
using Efilir.Core.Tools;
using Efilir.Core.Types;

namespace Efilir.Core.PredefinedCells.Cells
{
    public class PredefinedCell : IBaseCell
    {
        public Coordinate Position { get; set; }
        public AngleRotation CurrentRotate { get; set; }

        private PredefinedCellGameArea _gameArea;
        private Vector _realPosition;
        private Vector _velocityDirection;

        public PredefinedCell(PredefinedCellGameArea gameArea, Coordinate position)
        {
            _gameArea = gameArea;
            Position = position;

            _realPosition = new Vector(position.X, position.Y);
            _velocityDirection = GetRandomRotation();
        }

        public void MakeTurn(IGenericGameArea gameArea)
        {
            double timeDelta = 0.1;
            Vector newAcceleration = CalculateNewAcceleration();
            Vector newVelocity = _velocityDirection + newAcceleration * timeDelta;
            var newPosition = _realPosition + newVelocity * timeDelta;
            _realPosition = RoundPosition(newPosition);
            Position = _realPosition.ToCoordinate();

            var wallType = _gameArea.GetWallType(newPosition);
            if (wallType != WallType.Undefined)
            {
                double pushCoefficient = -0.5;
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

        public Vector CalculateNewAcceleration()
        {
            Vector newDirection = new Vector(0, 0);

            foreach (PredefinedCell predefinedCell in _gameArea.PredefinedCells)
            {
                Coordinate distance = predefinedCell.Position - Position;
                newDirection += new Vector(CalcMoveVector(distance.X), CalcMoveVector(distance.Y));
            }

            return newDirection;
        }

        private double CalcMoveVector(double distance)
        {
            if (Math.Abs(distance) < 1)
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

        private Vector GetRandomRotation()
        {
            var rotation = new AngleRotation(GlobalRand.Next(8));
            (int x, int y) = rotation.GetRotation();
            return new Vector(x, y);
        }
    }
}