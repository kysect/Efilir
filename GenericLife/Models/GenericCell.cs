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

        public FieldPosition Position { get; set; }
        private int CurrentRotate { get; set; }
        public Color GetColor()
        {
            if (Health == 0)
                return CellColorGenerator.DeadCell();
            return CellColorGenerator.HealthIndicator(Health);
        }

        public int MoveCommand(int commandRotate)
        {
            int actualRotate = (CurrentRotate + commandRotate) % 8;
            var movingCoord = AngleRotation.GetRotation(actualRotate);
            var newPos = Position + movingCoord;
            var directionCellState = _fieldModel.GetPointType(newPos);

            if (directionCellState == PointType.Food)
            {
                //_fieldModel.GetFoodCell(newX, newY);
            }

            return 0;

        }
    }
}