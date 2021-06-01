using System;
using System.Collections.Generic;
using System.Linq;
using Efilir.Core.Cells;
using Efilir.Core.Environment;
using Efilir.Core.Generics.Environment;
using Efilir.Core.Tools;
using Efilir.Core.Types;

namespace Efilir.Core.PredefinedCells.Cells
{
    public class PredefinedCell : IBaseCell
    {
        public Coordinate Position { get; set; }
        public AngleRotation CurrentRotate { get; set; }

        private GameArea _gameArea;
        private Vector _direction;
        private Vector _realPosition;

        public PredefinedCell(GameArea gameArea, Coordinate position)
        {
            _gameArea = gameArea;
            Position = position;

            _realPosition = new Vector(position.X, position.Y);
            _direction = GetRandomRotation();
        }

        public void MakeTurn(IGenericGameArea gameArea)
        {
            Vector newDirection = CalculateNewDirection();

            _gameArea.RemoveCell(this);
            _realPosition = RoundPosition(_realPosition + newDirection with {X = newDirection.X / newDirection.Length(), Y = newDirection.Y / newDirection.Length()});
            Position = _realPosition.ToCoordinate();
            _gameArea.AddCell(this);

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

            _direction = newDirection;
        }

        public Vector CalculateNewDirection()
        {
            int maxLength = 100000;

            List<PredefinedCell> predefinedCells = _gameArea.Cells
                .OfType<PredefinedCell>()
                .Where(c => Position.Distance(c.Position) < maxLength)
                .ToList();

            Vector newDirection = _direction;

            foreach (PredefinedCell predefinedCell in predefinedCells)
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
            return 5.0 / distance /*/ Math.Abs(distance)*/;

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