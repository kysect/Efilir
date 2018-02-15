using System.Windows.Media;
using GenericLife.Declaration;
using GenericLife.Tools;
using GenericLife.Types;

namespace GenericLife.Models.Cells
{
    public class GenericCell : ILiveCell
    {
        private int _health;

        public GenericCell()
        {
            CurrentRotate = new AngleRotation(0);
            Health = 100;
        }

        private ICellBrain _brain;
        public ICellBrain Brain
        {
            get => _brain;
            set
            {
                _brain = value;
                _brain.Cell = this;
            }
        }

        public AngleRotation CurrentRotate { get; set; }
        public ICellField FieldModel { get; set; }

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
            var posMoveTo = GetCellOnWay(commandRotate);
            var positionType = FieldModel.GetPointType(posMoveTo);

            if (positionType == PointType.Food)
            {
                var cellOnWay = FieldModel.GetCellOnPosition(posMoveTo);
                FieldModel.Foods.Remove(cellOnWay as FoodCell);
                Health += FoodCell.FoodHealthIncome;
                return;
            }

            if (positionType == PointType.Cell)
            {
                //Attack?
            }
        }

        public FieldPosition GetCellOnWay(int commandRotate)
        {
            var actualRotate = CurrentRotate + commandRotate;
            var newPosition = actualRotate.GetRotation();
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