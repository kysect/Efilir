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

        public IBaseCell[,] CellsPosition { get; private set; } = new IBaseCell[100, 100];

        public CellFieldModel()
        {
            DeleteAllElements();
            //for (int i = 0; i < Configuration.FoodCount; i++)
            //    AddFood();
        }

        public void DeleteAllElements()
        {
            CellsPosition = new IBaseCell[100, 100];
            Cells = new List<IGeneticCell>();
            Foods = new List<FoodCell>();
        }

        private List<IGeneticCell> Cells { get; set; }

        //TODO: remove food list
        private List<FoodCell> Foods { get; set; }

        public IBaseCell GetCellOnPosition(FieldPosition position)
        {
            if (position.X < 0 || position.X >= Configuration.FieldSize
                               || position.Y < 0
                               || position.Y >= Configuration.FieldSize)
                return new WallCell
                {
                    Position = new FieldPosition(-1, -1)
                };

            var cCell = CellsPosition[position.Y, position.X];
            return cCell;
            
            //var isFood = Foods.FirstOrDefault(c => c.Position.X == position.X
            //                                       && c.Position.Y == position.Y);

            //if (isFood != null) return isFood;

            //var cell = Cells.FirstOrDefault(c => c.Position.X == position.X
            //                                     && c.Position.Y == position.Y);
            //return cell;
        }

        public void MakeCellsMove()
        {
            UpdateFoodCount();
            foreach (var cell in Cells)
            {
                cell.TurnAction();
                //TODO:Remove dead
                //if (cell.IsAlive() == false) Cells.Remove(cell);
            }

            //UpdateFoodCount();
        }

        public void InitializeLiveCells(IEnumerable<IGeneticCell> cellsList)
        {
            //TODO: check for clean
            //Cells.Clear();
            foreach (var liveCell in cellsList)
            {
                liveCell.Position = GetEmptyPosition();
                liveCell.FieldModel = this;
                Cells.Add(liveCell);
                AddCellToField(liveCell);
            }
        }

        public bool AliveLessThanEight()
        {
            return Cells.Count(c => c.Health > 0) <= 8;
        }

        public void RemoveFoodCell(FoodCell cell)
        {
            Foods.Remove(cell);
            CellsPosition[cell.Position.Y, cell.Position.X] = null;
            AddFood();
        }

        public IEnumerable<IBaseCell> GetAllCells()
        {
            //TODO: remove
            return new List<IBaseCell>(Cells)
                .Union(Foods);
        }

        public List<IGeneticCell> GetAllLiveCells()
        {
            //TODO: remove
            return Cells;
        }

        private FieldPosition GetEmptyPosition()
        {
            FieldPosition newPos;
            do
            {
                newPos = GlobalRand.GeneratePosition();
            } while (GetCellOnPosition(newPos) != null);
            return newPos;
        }

        private void AddFood()
        {
            var pos = GetEmptyPosition();
            var newFood = new FoodCell(pos) {FieldModel = this};

            Foods.Add(newFood);
            AddCellToField(newFood);
        }

        private void UpdateFoodCount()
        {
            while (Foods.Count < FoodCount) AddFood();
        }

        private void AddCellToField(IBaseCell cell)
        {
            CellsPosition[cell.Position.Y, cell.Position.X] = cell;
        }
    }
}