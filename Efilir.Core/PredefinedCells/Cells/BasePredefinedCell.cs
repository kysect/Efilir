using System;
using Efilir.Core.Cells;
using Efilir.Core.Generics.Environment;
using Efilir.Core.Tools;
using Efilir.Core.Types;

namespace Efilir.Core.PredefinedCells.Cells
{
    public abstract class BasePredefinedCell : IBaseCell
    {
        public Coordinate Position { get; set; }
        public Vector RealPosition { get; private set; }
        public AngleRotation CurrentRotate { get; set; }
        public PredefinedCellType CellType { get; }

        protected readonly PredefinedCellGameArea GameArea;
        protected Vector VelocityDirection;

        public BasePredefinedCell(PredefinedCellGameArea gameArea, Vector position, PredefinedCellType cellType, Vector velocityDirection)
        {
            GameArea = gameArea;
            RealPosition = position;
            Position = RealPosition.ToCoordinate();
            CellType = cellType;
            VelocityDirection = velocityDirection;
        }

        public void MakeTurn(IGenericGameArea gameArea)
        {
            double timeDelta = Configuration.RecalculationRoundDelta;

            Vector newVelocity = RecalculateVelocity();
            newVelocity = newVelocity * Configuration.PredefinedCellVelocity / newVelocity.Length();

            Vector newPosition = RealPosition + newVelocity * timeDelta;
            RealPosition = RoundPosition(newPosition);
            Position = RealPosition.ToCoordinate();

            VelocityDirection = HandleWallInteraction(newPosition, VelocityDirection);
        }

        protected abstract Vector RecalculateVelocity();

        protected Vector HandleWallInteraction(Vector newPosition, Vector newVelocity)
        {
            WallType wallType = GameArea.GetWallType(newPosition);
            if (wallType != WallType.Undefined)
            {
                double pushCoefficient = -Configuration.WallPushCoefficient;

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

            return newVelocity;
        }

        protected double AngleToObject(Vector cellMoveVector, Vector vectorToObject)
        {
            double velocityAngle = Math.Atan(cellMoveVector.Y / cellMoveVector.X);
            double toObjectAngle = Math.Atan(vectorToObject.Y / vectorToObject.X);

            double delta = toObjectAngle - velocityAngle;
            if (delta > Math.PI)
                delta -= Math.PI * 2;


            return delta;
        }

        private Vector RoundPosition(Vector position)
        {
            double x = position.X;
            if (x < 0)
                x = 0;
            if (x >= GameArea.AreaSize)
                x = GameArea.AreaSize - 1;

            double y = position.Y;
            if (y < 0)
                y = 0;
            if (y >= GameArea.AreaSize)
                y = GameArea.AreaSize - 1;

            return new Vector(x, y);
        }

        public override string ToString()
        {
            return $"PredefinedCell on {Position} with angle {CurrentRotate} age";
        }
    }
}