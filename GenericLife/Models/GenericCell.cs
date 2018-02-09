using System.Collections.Generic;
using System.Windows.Media;
using GenericLife.Services;
using GenericLife.Tools;

namespace GenericLife.Models
{
    public class GenericCell : IBaseCell
    {
        private readonly CellFieldService _fieldService;

        public GenericCell(CellFieldService fieldService, int positionX, int positionY)
        {
            _fieldService = fieldService;
            PositionX = positionX;
            PositionY = positionY;
            CurrentRotate = 0;
            HitPoint = 100;

            ActionCommandList = new List<int>();
        }

        public GenericCell(CellFieldService fieldService, int positionX, int positionY, List<int> commandList)
        {
            _fieldService = fieldService;
            PositionX = positionX;
            PositionY = positionY;
            CurrentRotate = 0;
            HitPoint = 100;

            ActionCommandList = commandList;
        }

        public List<int> ActionCommandList { get; set; }

        public int HitPoint { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int CurrentRotate { get; set; }
        public Color GetColor()
        {
            if (HitPoint == 0)
                return new Color
                {
                    R = byte.MaxValue,
                    G = byte.MaxValue,
                    B = byte.MaxValue
                };

            var g = HitPoint * 2 < byte.MaxValue ? HitPoint * 2 : byte.MaxValue;
            var r = 200 - HitPoint * 2 > 0 ? 200 - HitPoint * 2 : 0;
            return new Color
            {
                G = (byte) g,
                R = (byte) r,
                B = 0
            };
        }

        public int MoveCommand(int commandRotate)
        {
            int actualRotate = (CurrentRotate + commandRotate) % 8;
            var movingCoord = AngleRotation.GetRotation(actualRotate);
            int newX = PositionX + movingCoord.x;
            int newY = PositionY + movingCoord.y;
            var directionCellState = _fieldService.GetPointType(newX, newY);

            if (directionCellState == PointType.Food)
            {
                _fieldService.GetFoodCell(newX, newY);
            }

            return 0;

        }
    }
}