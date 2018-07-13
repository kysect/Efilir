using System.Collections.Generic;
using GenericLife.Interfaces;
using GenericLife.Tools;
using GenericLife.Types;

namespace GenericLife.Models.Cells
{
    public class GenericCell : IGenericCell
    {
        private int _health;

        public GenericCell(List<int> commandList)
        {
            CurrentRotate = new AngleRotation(0);
            Health = 100;
            Brain = new CellBrain(this, commandList);
        }

        public int Generation { get; set; }
        public int Breed { get; set; }

        public ICellBrain Brain { get; }

        public AngleRotation CurrentRotate { get; set; }
        public GameArea Field { get; set; }

        public int Health
        {
            get => _health;
            set => _health = value > 100 ? 100 : value;
        }

        public int Age { get; set; }

        public void TurnAction()
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
            var cellType = Field.GetCellOnPosition(targetPosition)?.GetPointType() ?? PointType.Void;

            if (cellType == PointType.Food)
            {
                var cellOnWay = Field.GetCellOnPosition(targetPosition) as FoodCell;
                Health += cellOnWay.HealthIncome();
                Field.RemoveCell(cellOnWay.Position);
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
            return $"Simple cell with {Health} health and {Age} age";
        }

        private void IncreaseAge()
        {
            Age++;
            Health--;
        }
    }
}