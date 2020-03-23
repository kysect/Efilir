using System.Collections.Generic;
using System.Linq;
using GenericLife.Core.Cells;
using GenericLife.Core.Tools;
using GenericLife.Core.Types;

namespace GenericLife.Core.Environment
{
    public class SimulationManger
    {
        private const int FoodCount = Configuration.FoodCount;
        private readonly GameArea _gameArea = new GameArea();

        public SimulationManger()
        {
            DeleteAllElements();
        }
        public SimulationManger(int[,] cells)
        {
            DeleteAllElements(cells);
        }

        public List<IGenericCell> GetAllGenericCells()
        {
            return _gameArea.SelectIf<IGenericCell>().ToList();
        }

        public void DeleteAllElements()
        {
            _gameArea.CleanField();
        }
        public void DeleteAllElements(int[,] cells)
        {
            _gameArea.CleanField(cells);
        }

        public IBaseCell[,] GetAllCells()
        {
            return _gameArea.Cells;
        }

        public void MakeCellsMove()
        {
            UpdateFoodCount();
            foreach (var cell in _gameArea.SelectIf<IBaseCell>())
                cell.MakeTurn(_gameArea);

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
            foreach (IGenericCell liveCell in cellsList)
            {
                liveCell.Position = _gameArea.GetEmptyPosition();
                _gameArea.AddCell(liveCell);
            }
        }

        public int GetAliveCellCount()
        {
            return _gameArea.SelectIf<IGenericCell>().Count(c => c.Health > 0);
        }

        private void AddFood()
        {
            Coordinate pos = _gameArea.GetEmptyPosition();
            var newFood = new FoodCell(pos);
            _gameArea.AddCell(newFood);
        }

        private void UpdateFoodCount()
        {
            while (_gameArea.SelectIf<FoodCell>().Count() < FoodCount) AddFood();
        }
    }
}