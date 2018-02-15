using System.Collections.Generic;
using System.Linq;
using GenericLife.Declaration;
using GenericLife.Models.Cells;
using GenericLife.Tools;
using GenericLife.Types;

namespace GenericLife.Models
{
    public class CellFieldModel : ICellField
    {
        private const int FoodCount = Configuration.FoodCount;

        public CellFieldModel()
        {
            Cells = new List<ILiveCell>();
            Foods = new List<FoodCell>();
        }

        public List<ILiveCell> Cells { get; set; }
        public List<FoodCell> Foods { get; set; }

        public IBaseCell GetCellOnPosition(FieldPosition position)
        {
            var isFood = Foods.FirstOrDefault(c => c.Position.X == position.X
                                                   && c.Position.Y == position.Y);

            if (isFood != null) return isFood;
            var cell = Cells.FirstOrDefault(c => c.Position.X == position.X
                                                 && c.Position.Y == position.Y);
            return cell;
        }

        public PointType GetPointType(FieldPosition position)
        {
            if (position.X < 0 || position.X >= Configuration.FieldSize
                               || position.Y < 0
                               || position.Y >= Configuration.FieldSize)
                return PointType.Wall;

            var cell = GetCellOnPosition(position);

            if (cell is FoodCell) return PointType.Food;
            if (cell is ILiveCell) return PointType.Cell;
            return PointType.Void;
        }

        public void RandomMove()
        {
            foreach (var cell in Cells) cell.TurnAction();

            UpdateFoodCount();
        }

        public void AddCell(ILiveCell cell)
        {
            cell.Position = GetEmptyPosition();
            cell.FieldModel = this;
            Cells.Add(cell);
        }

        public bool AliveLessThanEight()
        {
            return Cells.Count(c => c.Health > 0) <= 8;
        }

        private FieldPosition GetEmptyPosition()
        {
            int x, y;
            do
            {
                x = GlobalRand.Next(Configuration.FieldSize);
                y = GlobalRand.Next(Configuration.FieldSize);
            } while (GetPointType(new FieldPosition(x, y)) != PointType.Void);

            return new FieldPosition(x, y);
        }

        private void AddFood()
        {
            var pos = GetEmptyPosition();
            Foods.Add(new FoodCell(pos) {FieldModel = this});
        }

        private void UpdateFoodCount()
        {
            while (Foods.Count < FoodCount) AddFood();
        }
    }
}