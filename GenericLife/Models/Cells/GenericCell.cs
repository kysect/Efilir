using System.Collections.Generic;
using System.Windows.Media;
using GenericLife.Declaration;
using GenericLife.Tools;

namespace GenericLife.Models.Cells
{
    public class GenericCell : ILiveCell
    {
        public CellBrain Brain { get; set; }
        public ICellField FieldModel { get; set; }
        private int _currentRotation;
        private int _health;

        public GenericCell(CellFieldModel fieldModel, FieldPosition position)
        {
            FieldModel = fieldModel;
            Position = position;
            CurrentRotate = 0;
            Health = 100;
            Brain = new CellBrain()
            {
                Cell = this,
                CommandList = GlobalRand.GenerateCommandList(),
                Field = FieldModel
            };
        }

        public int CurrentRotate
        {
            get => _currentRotation;
            set => _currentRotation = value % 8;
        }

        public int Health
        {
            get => _health;
            set => _health = value > 100 ? 100 : value;
        }

        public int Age { get; set; }

        public void TurnAction()
        {
            if (!IsAlive) return;

            Brain.MakeTurn();
            IncreaseAge();
        }

        public bool IsAlive => Health > 0;

        public FieldPosition Position { get; set; }

        public Color GetColor()
        {
            if (Health <= 0)
                return CellColorGenerator.DeadCell();
            return CellColorGenerator.HealthIndicator(Health);
        }

        public void MoveCommand(int commandRotate)
        {
            ActionCommand(commandRotate);

            var newPos = GetCellOnWay(commandRotate);
            var directionCellState = FieldModel.GetPointType(newPos);

            if (directionCellState == PointType.Void) Position = newPos;
        }

        public void ActionCommand(int commandRotate)
        {
            var newPos = GetCellOnWay(commandRotate);
            var directionCellState = FieldModel.GetPointType(newPos);

            if (directionCellState == PointType.Food)
            {
                var cellOnWay = FieldModel.GetCellOnPosition(newPos);
                FieldModel.Foods.Remove(cellOnWay as FoodCell);
                Health += FoodCell.FoodHealthIncome;
                return;
            }

            if (directionCellState == PointType.Cell)
            {
                //Attack?
            }
        }

        private FieldPosition GetCellOnWay(int commandRotate)
        {
            var actualRotate = (CurrentRotate + commandRotate) % 8;
            var newPosition = AngleRotation.GetRotation(actualRotate);
            return Position + newPosition;
        }

        private void IncreaseAge()
        {
            Age++;
            Health--;
        }

        public override string ToString()
        {
            return $"Simple cell with {Health} health and {Age} age";
        }
    }
}