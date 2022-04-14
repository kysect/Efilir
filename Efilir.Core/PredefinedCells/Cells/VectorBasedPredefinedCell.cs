using System.Collections.Generic;
using Efilir.Core.Tools;
using Efilir.Core.Types;

namespace Efilir.Core.PredefinedCells.Cells
{
    public class VectorBasedPredefinedCell : BasePredefinedCell
    {
        public VectorBasedPredefinedCell(PredefinedCellGameArea gameArea, Vector position, Vector velocity, PredefinedCellType cellType) : base(gameArea, position, cellType, velocity)
        {
        }

        protected override Vector RecalculateVelocity()
        {
            double timeDelta = Configuration.RecalculationRoundDelta;

            Vector newAcceleration = CalculateNewAccelerationWithoutDistanceForSteps();
            Vector newVelocity = VelocityDirection + newAcceleration * timeDelta;
            return newVelocity;
        }

        private Vector CalculateNewAccelerationWithoutDistance()
        {
            Vector newDirection = new Vector(0, 0);

            foreach ((PredefinedCellType type, Vector predefinedCellPosition) in GameArea.PreviousCellPosition)
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

            foreach (List<(PredefinedCellType, Vector)> stepOnIteration in GameArea.PreviousSteps)
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

        private bool IsCellOnWay(Vector moveDirection)
        {
            Angle delta = VelocityDirection.AngleTo(moveDirection);
            return delta.Abs() < Configuration.CellVisibleAngle;
        }
    }
}