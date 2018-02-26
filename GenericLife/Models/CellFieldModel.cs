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
            Cells = new List<IGeneticCell>();
            Foods = new List<FoodCell>();
        }

        private List<IGeneticCell> Cells { get; set; }

        private List<FoodCell> Foods { get; set; }
        //public List<IGeneticCell> DeadCells { get; set; }

        public IBaseCell GetCellOnPosition(FieldPosition position)
        {
            if (position.X < 0 || position.X >= Configuration.FieldSize
                               || position.Y < 0
                               || position.Y >= Configuration.FieldSize)
                return new WallCell
                {
                    Position = new FieldPosition(-1, -1)
                };

            var isFood = Foods.FirstOrDefault(c => c.Position.X == position.X
                                                   && c.Position.Y == position.Y);

            if (isFood != null) return isFood;

            var cell = Cells.FirstOrDefault(c => c.Position.X == position.X
                                                 && c.Position.Y == position.Y);
            return cell;
        }

        public void MakeCellsMove()
        {
            foreach (var cell in Cells)
            {
                cell.TurnAction();
                //TODO:Remove dead
                //if (cell.IsAlive() == false) Cells.Remove(cell);
            }

            UpdateFoodCount();
        }

        public void InitializeLiveCells(IEnumerable<IGeneticCell> cellsList)
        {
            Cells.Clear();
            foreach (var liveCell in cellsList)
            {
                liveCell.Position = GetEmptyPosition();
                liveCell.FieldModel = this;
                Cells.Add(liveCell);
            }
        }

        public bool AliveLessThanEight()
        {
            return Cells.Count(c => c.Health > 0) <= 8;
        }

        public void RemoveFoodCell(FoodCell cell)
        {
            Foods.Remove(cell);
        }

        public IEnumerable<IBaseCell> GetAllCells()
        {
            return new List<IBaseCell>(Cells)
                .Union(Foods);
        }

        public List<IGeneticCell> GetAllLiveCells()
        {
            return Cells;
        }

        private FieldPosition GetEmptyPosition()
        {
            int x, y;
            do
            {
                x = GlobalRand.Next(Configuration.FieldSize);
                y = GlobalRand.Next(Configuration.FieldSize);
            } while (GetCellOnPosition(new FieldPosition(x, y)) != null);

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