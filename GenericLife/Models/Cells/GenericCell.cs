using System.Collections.Generic;
using GenericLife.Interfaces;
using GenericLife.Tools;
using GenericLife.Types;

namespace GenericLife.Models.Cells
{
    public class GenericCell : IGenericCell
    {
        public GenericCell(CellBrain brain)
        {
            CurrentRotate = new AngleRotation(0);
            Health = 100;
            Brain = brain;
            brain.Cell = this;
        }

        public ICellBrain Brain { get; }
        public AngleRotation CurrentRotate { get; set; }
        public GameArea Field { get; set; }
        public int Health { get; private set; }
        public int Age { get; private set; }

        public void MakeTurn()
        {
            if (!IsAlive()) return;

            Brain.MakeTurn();
            IncreaseAge();
        }

        public bool IsAlive()
        {
            return Health > 0;
        }

        public Coordinate Position { get; set; }

        public void MoveCommand(int commandRotate)
        {
            ActionCommand(commandRotate);

            var targetPosition = AnalyzePosition(commandRotate);
            var targetCell = Field.GetCellOnPosition(targetPosition);

            if (targetCell == null) Position = targetPosition;
        }

        public void ActionCommand(int commandRotate)
        {
            //TODO: return cell, not only type
            var targetPosition = AnalyzePosition(commandRotate);
            var cellOnWay = Field.GetCellOnPosition(targetPosition);
            if (cellOnWay == null)
            {
                return;
            }

            var cellType = cellOnWay?.GetPointType();
            if (cellType == PointType.Food)
            {
                Health += (cellOnWay as FoodCell).HealthIncome();
                Field.RemoveCell(cellOnWay);
                return;
            }

            if (cellType == PointType.Cell)
            {
                //Attack?
            }
        }

        public Coordinate AnalyzePosition(int commandRotate)
        {
            var actualRotate = CurrentRotate + commandRotate;
            var newPosition = actualRotate.GetRotation();
            return Position + newPosition;
        }

        public override string ToString()
        {
            return $"G{Brain.Breed} with {Health} health and {Age} age";
        }

        private void IncreaseAge()
        {
            Age++;
            Health--;
        }
    }
}