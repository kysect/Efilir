using System.Collections.Generic;
using System.Linq;
using GenericLife.Core.CellAbstractions;
using GenericLife.Core.Models.Cells;
using GenericLife.Core.Tools;

namespace GenericLife.Core.Models
{
    public class SimulationManger
    {
        private const int FoodCount = Configuration.FoodCount;
        private readonly GameArea _gameArea = new GameArea();

        public SimulationManger()
        {
            DeleteAllElements();
        }

        public List<IGenericCell> GetAllGenericCells()
        {
            return _gameArea.Select<IGenericCell>().ToList();
        }

        public void DeleteAllElements()
        {
            _gameArea.CleanField();
        }

        public IBaseCell[,] GetAllCells()
        {
            return _gameArea.Cells;
        }

        public void MakeCellsMove()
        {
            UpdateFoodCount();
            foreach (var cell in _gameArea.Select<IBaseCell>())
                cell.MakeTurn();

            //var deadCellList = Cells.Where(c => c.IsAlive() == false).ToList();
            //foreach (var deadCell in deadCellList)
            //{
            //    Cells.Remove(deadCell);

            //    //TODO: replace dead cell with food or ...??
            //    //_gameArea.RemoveCell(deadCell);
            //}
        }

        public void InitializeLiveCells(IEnumerable<IGenericCell> cellsList)
        {
            foreach (var liveCell in cellsList)
            {
                liveCell.Position = _gameArea.GetEmptyPosition();
                liveCell.Field = _gameArea;
                _gameArea.AddCell(liveCell);
            }
        }

        public bool AliveLessThanEight()
        {
            return _gameArea.Select<IGenericCell>().Count(c => c.Health > 0) <= 8;
        }

        private void AddFood()
        {
            var pos = _gameArea.GetEmptyPosition();
            var newFood = new FoodCell(pos) {Field = _gameArea};

            _gameArea.AddCell(newFood);
        }

        private void UpdateFoodCount()
        {
            while (_gameArea.Select<IFoodCell>().Count() < FoodCount) AddFood();
        }
    }
}