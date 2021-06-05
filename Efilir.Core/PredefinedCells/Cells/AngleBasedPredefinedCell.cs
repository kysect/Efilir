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

            var angle = CalcRotateAngle();
            var newX = VelocityDirection.X * Math.Cos(angle) - VelocityDirection.Y * Math.Sin(angle);
            var newY = VelocityDirection.X * Math.Sin(angle) + VelocityDirection.Y * Math.Cos(angle);
            
            return new Vector(newX, newY);
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

                double angleToObject = AngleToObject(VelocityDirection, moveDirection);
                if (Math.Abs(angleToObject) > Configuration.CellVisibleAngle)
                    continue;

                if (angleToObject > 0)
                    weight++;

                if (angleToObject < 0)
                    weight--;
                
                //if (type == CellType)
                    //newDirection += moveDirection /*/ moveDirection.Length()*/;
                //else
                //    newDirection -= moveDirection / moveDirection.Length();
            }

            if (weight > 0)
                return Configuration.TurnAngleChange / 180.0;

            if (weight < 0)
                return -Configuration.TurnAngleChange / 180;

            return 0;
        }
    }
}