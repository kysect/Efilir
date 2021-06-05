using System;
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

            double angle = CalcRotateAngle();

            return VelocityDirection.RotateToAngle(angle);
        }

        private double CalcRotateAngle()
        {
            int weight = 0;

            foreach (List<(PredefinedCellType, Vector)> stepOnIteration in GameArea.PreviousSteps)
            foreach ((PredefinedCellType type, Vector predefinedCellPosition) in stepOnIteration)
            {
                Vector moveDirection = predefinedCellPosition - RealPosition;

                if (moveDirection.Length() > Configuration.MaxLengthForInteraction || moveDirection.Length() < double.Epsilon)
                    continue;

                double angleToObject = VelocityDirection.AngleTo(moveDirection);
                if (Math.Abs(angleToObject) > Configuration.CellVisibleAngle)
                    continue;

                if (angleToObject > 0)
                    weight++;

                if (angleToObject < 0)
                    weight--;
            }

            if (weight > 0)
                return Configuration.TurnAngleChange / 180.0;

            if (weight < 0)
                return -Configuration.TurnAngleChange / 180;

            return 0;
        }
    }
}