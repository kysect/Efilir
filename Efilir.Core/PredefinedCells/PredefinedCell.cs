using System;
using System.Collections.Generic;
using Efilir.Core.Cells;
using Efilir.Core.Generics.Environment;
using Efilir.Core.Tools;
using Efilir.Core.Types;

namespace Efilir.Core.PredefinedCells
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
            double timeDelta = Configuration.RecalculationRoundDelta;

            Vector newVelocity = RecalculateVelocity();
            newVelocity = newVelocity  * Configuration.PredefinedCellVelocity / newVelocity.Length();

            Vector newPosition = RealPosition + newVelocity * timeDelta;
            RealPosition = RoundPosition(newPosition);
            Position = RealPosition.ToCoordinate();

            _velocityDirection = HandleWallInteraction(newPosition, newVelocity);
        }

        private Vector RecalculateVelocity()
        {
            double timeDelta = Configuration.RecalculationRoundDelta;

            Vector newAcceleration = CalculateNewAccelerationWithoutDistanceForSteps();
            Vector newVelocity = _velocityDirection + newAcceleration * timeDelta;
            return newVelocity;
        }

        private Vector HandleWallInteraction(Vector newPosition, Vector newVelocity)
        {
            WallType wallType = _gameArea.GetWallType(newPosition);
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

        private Vector CalculateNewAccelerationWithoutDistance()
        {
            Vector newDirection = new Vector(0, 0);

            foreach ((PredefinedCellType type, Vector predefinedCellPosition) in _gameArea.PreviousCellPosition)
            {
                Vector moveDirection = predefinedCellPosition - RealPosition;

                if (moveDirection.Length() > Configuration.MaxLengthForInteraction || moveDirection.Length() < double.Epsilon)
                    continue;

                if (!IsCellOnWay(moveDirection))
                    continue;

                if (type == CellType)
                    newDirection += moveDirection / moveDirection.Length();
                //else
                //    newDirection -= moveDirection / moveDirection.Length();
            }

            if (newDirection.Length() < double.Epsilon)
                return newDirection;

            return newDirection / 2;
        }

        private Vector CalculateNewAccelerationWithoutDistanceForSteps()
        {
            Vector newDirection = new Vector(0, 0);

            foreach (List<(PredefinedCellType, Vector)> stepOnIteration in _gameArea.PreviousSteps)
            foreach ((PredefinedCellType type, Vector predefinedCellPosition) in stepOnIteration)
            {
                Vector moveDirection = predefinedCellPosition - RealPosition;

                if (moveDirection.Length() > Configuration.MaxLengthForInteraction || moveDirection.Length() < double.Epsilon)
                    continue;

                if (!IsCellOnWay(moveDirection))
                    continue;

                if (type == CellType)
                    newDirection += moveDirection /*/ moveDirection.Length()*/;
                //else
                //    newDirection -= moveDirection / moveDirection.Length();
            }

            if (newDirection.Length() < double.Epsilon)
                return newDirection;

            return newDirection / newDirection.Length() / 3;
        }

        private double AngleToObject(Vector cellMoveVector, Vector vectorToObject)
        {
            double velocityAngle = Math.Atan(cellMoveVector.Y / cellMoveVector.X);
            double toObjectAngle = Math.Atan(vectorToObject.Y / vectorToObject.X);

            double delta = toObjectAngle - velocityAngle;
            if (delta > Math.PI)
                delta -= Math.PI * 2;


            return delta;
        }

        private bool IsCellOnWay(Vector moveDirection)
        {
            double delta = AngleToObject(_velocityDirection, moveDirection);
            return Math.Abs(delta) <= Configuration.CellVisibleAngle;
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