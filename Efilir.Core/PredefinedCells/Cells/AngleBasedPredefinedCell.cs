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

            var angle = CalcRotateAngle();

            return VelocityDirection.RotateToAngle(angle);
        }

        private Angle CalcRotateAngle()
        {
            int weight = 0;

            foreach (List<(PredefinedCellType, Vector)> stepOnIteration in GameArea.PreviousSteps)
            foreach ((PredefinedCellType type, Vector predefinedCellPosition) in stepOnIteration)
            {
                Vector moveDirection = predefinedCellPosition - RealPosition;

                if (moveDirection.Length() > Configuration.MaxLengthForInteraction || moveDirection.Length() < double.Epsilon)
                    continue;

                Angle angleToObject = VelocityDirection.AngleTo(moveDirection);
                if (angleToObject > Configuration.CellVisibleAngle)
                    continue;

                if (angleToObject > Angle.Zero)
                    weight++;

                if (angleToObject < Angle.Zero)
                    weight--;
            }

            if (weight > 0)
                return Angle.FromDegree(Configuration.TurnAngleChange);
            if (weight < 0)
                return Angle.FromDegree(-Configuration.TurnAngleChange);

            return Angle.FromDegree(0);
        }
    }
}