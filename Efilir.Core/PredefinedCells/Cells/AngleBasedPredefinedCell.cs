using System.Collections.Generic;
using Efilir.Core.Tools;
using Efilir.Core.Types;

namespace Efilir.Core.PredefinedCells.Cells
{
    public class AngleBasedPredefinedCell : BasePredefinedCell
    {
        public AngleBasedPredefinedCell(PredefinedCellGameArea gameArea, Vector position, Vector velocity, PredefinedCellType cellType) : base(gameArea, position, cellType, velocity)
        {
        }

        protected override Vector RecalculateVelocity()
        {
            double timeDelta = Configuration.RecalculationRoundDelta;

            Angle angle = CalcRotateAngle();

            return VelocityDirection.RotateToAngle(angle);
        }

        private Angle CalcRotateAngle()
        {
            double weight = 0;

            int stepNumber = GameArea.PreviousSteps.Count + 1;
            foreach (List<(PredefinedCellType, Vector)> stepOnIteration in GameArea.PreviousSteps)
            {
                stepNumber--;
                foreach ((PredefinedCellType type, Vector predefinedCellPosition) in stepOnIteration)
                {
                    Vector moveDirection = predefinedCellPosition - RealPosition;

                    if (moveDirection.Length() > Configuration.MaxLengthForInteraction || moveDirection.Length() < double.Epsilon)
                        continue;

                    Angle angleToObject = VelocityDirection.AngleTo(moveDirection);
                    if (angleToObject > Configuration.CellVisibleAngle)
                        continue;

                    if (angleToObject > Angle.Zero)
                        weight += 1.0 / stepNumber;

                    if (angleToObject < Angle.Zero)
                        weight -= 1.0 / stepNumber;
                }
            }

            if (weight > 0)
                return GenerateRandomAngle();
            if (weight < 0)
                return GenerateRandomAngle() * -1;

            return Angle.FromDegree(0);
        }

        private Angle GenerateRandomAngle()
        {
            return Configuration.TurnAngleChange * ((GlobalRand.Next(100) + 1.0) / 100);
        }
    }
}