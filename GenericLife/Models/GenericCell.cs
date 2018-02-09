using System.Collections.Generic;
using System.Windows.Media;
using GenericLife.Declaration;
using GenericLife.Tools;

namespace GenericLife.Models
{
    public class GenericCell : ILiveCell
    {
        private readonly CellFieldModel _fieldModel;

        public GenericCell(CellFieldModel fieldModel, FieldPosition position)
        {
            _fieldModel = fieldModel;
            Position = position;
            CurrentRotate = 0;
            Health = 100;

            ActionCommandList = new List<int>();
        }

        public GenericCell(CellFieldModel fieldModel, FieldPosition position, List<int> commandList)
        {
            _fieldModel = fieldModel;
            Position = position;
            CurrentRotate = 0;
            Health = 100;

            ActionCommandList = commandList;
        }

        public List<int> ActionCommandList { get; set; }

        public int Health { get; set; }
        public int Age { get; set; }
        public void TurnAction()
        {
            throw new System.NotImplementedException();
        }

        private int _currentRotation;
        public FieldPosition Position { get; set; }

        public int CurrentRotate
        {
            get => _currentRotation;
            set => _currentRotation = value % 8;
        }

        public Color GetColor()
        {
            if (Health == 0)
                return CellColorGenerator.DeadCell();
            return CellColorGenerator.HealthIndicator(Health);
        }

        public int MoveCommand(int commandRotate)
        {
            ActionCommand(commandRotate);

            var newPos = GetCellOnWay(commandRotate);
            var directionCellState = _fieldModel.GetPointType(newPos);

            if (directionCellState == PointType.Void)
            {
                Position = newPos;
            }

            return 0;

        }

        public void ActionCommand(int commandRotate)
        {
            IncreaseAge();

            var newPos = GetCellOnWay(commandRotate);
            var directionCellState = _fieldModel.GetPointType(newPos);

            if (directionCellState == PointType.Food)
            {
                var cellOnWay = _fieldModel.GetCellOnPosition(newPos);
                _fieldModel.Foods.Remove(cellOnWay as FoodCell);
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
            int actualRotate = (CurrentRotate + commandRotate) % 8;
            var newPosition = AngleRotation.GetRotation(actualRotate);
            return Position + newPosition;
        }

        private void IncreaseAge()
        {
            Age++;
            Health--;
        }
    }
}