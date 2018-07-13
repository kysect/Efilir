using System.Collections.Generic;
using System.Linq;
using GenericLife.Interfaces;
using GenericLife.Models.Cells;
using GenericLife.Tools;

namespace GenericLife.Models
{
    public class SimulationManger
    {
        private const int FoodCount = Configuration.FoodCount;
        private readonly GameArea _gameArea = new GameArea();

        public SimulationManger()
        {
            DeleteAllElements();
        }

        private List<IGenericCell> Cells { get; set; }

        public void DeleteAllElements()
        {
            _gameArea.CleanField();
            Cells = new List<IGenericCell>();
        }

        public IBaseCell[,] GetAllCells()
        {
            return _gameArea.Cells;
        }

        public void MakeCellsMove()
        {
            UpdateFoodCount();
            foreach (var cell in Cells)
                cell.TurnAction();
            //TODO:Remove dead
            //if (cell.IsAlive() == false) Cells.Remove(cell);
        }

        public void InitializeLiveCells(IEnumerable<IGenericCell> cellsList)
        {
            //TODO: check for clean
            //Cells.Clear();
            foreach (var liveCell in cellsList)
            {
                liveCell.Position = _gameArea.GetEmptyPosition();
                liveCell.Field = _gameArea;
                Cells.Add(liveCell);
                AddCellToField(liveCell);
            }
        }

        public bool AliveLessThanEight()
        {
            return Cells.Count(c => c.Health > 0) <= 8;
        }

        public IEnumerable<IGenericCell> GetAllLiveCells()
        {
            //TODO: remove
            return Cells;
        }

        private void AddFood()
        {
            var pos = _gameArea.GetEmptyPosition();
            var newFood = new FoodCell(pos) {Field = _gameArea};

            AddCellToField(newFood);
        }

        private void UpdateFoodCount()
        {
            while (_gameArea.CurrentTypeCount<IFoodCell>() < FoodCount) AddFood();
        }

        private void AddCellToField(IBaseCell cell)
        {
            _gameArea.AddCell(cell);
        }
    }
}