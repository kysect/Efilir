using System.Collections.Generic;
using System.Linq;
using GenericLife.Declaration;
using GenericLife.Tools;

namespace GenericLife.Models
{
    public class CellFieldModel : ICellField
    {
        public List<ILiveCell> Cells { get; set; }
        public List<FoodCell> Foods { get; set; }
        public readonly int FieldSize = 100;

        public CellFieldModel()
        {
            Cells = new List<ILiveCell>();
            Foods = new List<FoodCell>();
        }

        private (int X, int Y) GetEmptyPosition()
        {
            int x, y;
            do
            {
                x = GlobalRand.Next(FieldSize);
                y = GlobalRand.Next(FieldSize);
            } while (GetPointType(x, y) != PointType.Void);

            return (x, y);
        }

        public void AddRandomCell()
        {
            var pos = GetEmptyPosition();
            Cells.Add(new SimpleCell(this, pos.X, pos.Y));
        }

        public void AddFood()
        {
            var pos = GetEmptyPosition();
            Foods.Add(new FoodCell(pos.X, pos.Y));
        }

        public IBaseCell GetCellOnPosition(int positionX, int positionY)
        {
            IBaseCell cell = Cells.FirstOrDefault(c => c.PositionX == positionX
                                                       && c.PositionY == positionY);
            if (cell != null)
            {
                return cell;
            }

            cell = Foods.FirstOrDefault(c => c.PositionX == positionX
                                             && c.PositionY == positionY);
            return cell;
        }

        public PointType GetPointType(int positionX, int positionY)
        {
            if (positionX < 0 || positionX >= FieldSize || positionY < 0 || positionY >= FieldSize)
                return PointType.OutOfRange;

            IBaseCell cell = GetCellOnPosition(positionX, positionY);
            if (cell is FoodCell) return PointType.Food;
            if (cell is ILiveCell) return PointType.Cell;
            return PointType.Void;
        }

        private void UpdateFoodCount()
        {
            while (Foods.Count < 200)
            {
                AddFood();
            }
        }

        public void RandomMove()
        {
            foreach (var cell in Cells)
            {
                cell.TurnAction();
            }

            UpdateFoodCount();
        }
    }
}