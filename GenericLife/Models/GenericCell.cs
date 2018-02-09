using System.Collections.Generic;
using System.Windows.Media;
using GenericLife.Declaration;
using GenericLife.Tools;

namespace GenericLife.Models
{
    public class GenericCell : ILiveCell
    {
        private readonly CellFieldModel _fieldModel;

        public GenericCell(CellFieldModel fieldModel, int positionX, int positionY)
        {
            _fieldModel = fieldModel;
            PositionX = positionX;
            PositionY = positionY;
            CurrentRotate = 0;
            Health = 100;

            ActionCommandList = new List<int>();
        }

        public GenericCell(CellFieldModel fieldModel, int positionX, int positionY, List<int> commandList)
        {
            _fieldModel = fieldModel;
            PositionX = positionX;
            PositionY = positionY;
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

        public int PositionX { get; set; }
        public int PositionY { get; set; }
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
            int newX = PositionX + movingCoord.x;
            int newY = PositionY + movingCoord.y;
            var directionCellState = _fieldModel.GetPointType(newX, newY);

            if (directionCellState == PointType.Food)
            {
                //_fieldModel.GetFoodCell(newX, newY);
            }

            return 0;

        }
    }
}