using System.Collections.Generic;
using System.Linq;
using GenericLife.Declaration;
using GenericLife.Models.Cells;
using GenericLife.Tools;

namespace GenericLife.Models
{
    public class CellFieldModel : ICellField
    {
        public const int FoodCount = 500;
        public readonly int FieldSize = 100;

        public CellFieldModel()
        {
            Cells = new List<ILiveCell>();
            Foods = new List<FoodCell>();
        }

        public bool IsAnyAlive
        {
            get { return Cells.Any(c => c.Health > 0); }
        }

        public bool IsAliveFew
        {
            get { return Cells.Count(c => c.Health > 0) <= 8; }
        }

        public List<ILiveCell> Cells { get; set; }
        public List<FoodCell> Foods { get; set; }

        public IBaseCell GetCellOnPosition(FieldPosition position)
        {
            IBaseCell cell = Cells.FirstOrDefault(c => c.Position.X == position.X
                                                       && c.Position.Y == position.Y);
            if (cell != null) return cell;

            cell = Foods.FirstOrDefault(c => c.Position.X == position.X
                                             && c.Position.Y == position.Y);
            return cell;
        }

        public PointType GetPointType(FieldPosition position)
        {
            if (position.X < 0 || position.X >= FieldSize || position.Y < 0 || position.Y >= FieldSize)
                return PointType.OutOfRange;

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

        private FieldPosition GetEmptyPosition()
        {
            int x, y;
            do
            {
                x = GlobalRand.Next(FieldSize);
                y = GlobalRand.Next(FieldSize);
            } while (GetPointType(new FieldPosition(x, y)) != PointType.Void);

            return new FieldPosition(x, y);
        }

        public void AddSimpleCell()
        {
            var pos = GetEmptyPosition();
            Cells.Add(new SimpleCell(this, pos));
        }

        public void AddGenericCell()
        {
            var pos = GetEmptyPosition();
            Cells.Add(new GenericCell(this, pos));
        }

        public void AddFood()
        {
            var pos = GetEmptyPosition();
            Foods.Add(new FoodCell(pos));
        }

        private void UpdateFoodCount()
        {
            while (Foods.Count < FoodCount) AddFood();
        }
    }
}