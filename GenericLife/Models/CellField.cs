using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericLife.Models
{
    public class CellField
    {
        public readonly List<SimpleCell> Cells;
        public readonly int Height;
        public readonly int Width;

        private readonly Random _rand;

        public CellField()
        {
            Cells = new List<SimpleCell>();
            _rand = new Random();
            Width = 100;
            Height = 100;
        }

        public PointType GetPointType(int positionX, int positionY)
        {
            if (positionX < 0 || positionX >= 100 || positionY < 0 || positionY >= 100)
                return PointType.OutOfRange;

            var isPoint = Cells.Any(c => c.PositionX == positionX && c.PositionY == positionY);
            return isPoint ? PointType.Cell : PointType.Void;
        }

        public void RandomMove()
        {
            foreach (var cell in Cells)
            {
                var correctMoving = new List<Action>();

                if (GetPointType(cell.PositionX, cell.PositionY - 1) == PointType.Void)
                    correctMoving.Add(() => { cell.PositionY -= 1; });
                if (GetPointType(cell.PositionX + 1, cell.PositionY) == PointType.Void)
                    correctMoving.Add(() => { cell.PositionX += 1; });
                if (GetPointType(cell.PositionX, cell.PositionY + 1) == PointType.Void)
                    correctMoving.Add(() => { cell.PositionY += 1; });
                if (GetPointType(cell.PositionX - 1, cell.PositionY) == PointType.Void)
                    correctMoving.Add(() => { cell.PositionX -= 1; });

                var type = _rand.Next(correctMoving.Count);
                if (correctMoving.Count != 0 && cell.HitPoint > 0)
                {
                    correctMoving[type].Invoke();
                    cell.HitPoint -= 1;
                }
            }
        }
    }
}