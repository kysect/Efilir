using System.Collections.Generic;
using System.Linq;
using Efilir.Core.Cells;
using Efilir.Core.Tools;
using Efilir.Core.Types;

namespace Efilir.Core.Environment
{
    public class LivingCellSimulationManger
    {
        private const int FoodCount = Configuration.FoodCount;
        private readonly GameArea _gameArea;

        public LivingCellSimulationManger()
        {
            _gameArea = new GameArea(Configuration.FieldSize);
        }

        public List<IGenericCell> GetAllGenericCells()
        {
            return _gameArea.GenericCells;
        }

        public void DeleteAllElements()
        {
            _gameArea.CleanField();
            _gameArea.GenerateRandomWall();
        }

        public void GenerateGameField(int[,] cells = null)
        {
            if (cells is null)
                _gameArea.GenerateRandomWall();
            else
                _gameArea.GenerateGameField(cells);
        }

        public IBaseCell[,] GetAllCells()
        {
            return _gameArea.Cells;
        }

        public void ProcessIteration()
        {
            UpdateFoodCount();
            UpdateTrapCount();
            foreach (IBaseCell cell in _gameArea.SelectIf<IBaseCell>())
            {
                Coordinate oldPosition = cell.Position;
                cell.MakeTurn(_gameArea);
                if (oldPosition != cell.Position)
                {
                    _gameArea.RemoveCell(oldPosition);
                    _gameArea.AddCell(cell);
                }
            }

            List<IGenericCell> deadCellList = _gameArea.SelectIf<IGenericCell>().Where(c => !c.IsAlive()).ToList();
            foreach (IGenericCell deadCell in deadCellList)
            {
                _gameArea.AddCell(new FoodCell(deadCell.Position));
            }
        }

        public void InitializeLiveCells(IEnumerable<IGenericCell> cellsList)
        {
            _gameArea.GenericCells = cellsList.ToList();
            foreach (IGenericCell liveCell in _gameArea.GenericCells)
            {
                liveCell.Position = _gameArea.GetEmptyPosition();
                _gameArea.AddCell(liveCell);
            }
        }

        public bool IsSimulationActive()
        {
            //TODO: move as argument
            return _gameArea.GenericCells.Count(c => c.IsAlive()) <= 8;
        }

        private void AddFood()
        {
            Coordinate pos = _gameArea.GetEmptyPosition();
            var newFood = new FoodCell(pos);
            _gameArea.AddCell(newFood);
        }

        private void UpdateFoodCount()
        {
            while (_gameArea.SelectIf<FoodCell>().Count() < FoodCount)
                AddFood();
        }

        private void AddTrap()
        {
            Coordinate pos = _gameArea.GetEmptyPosition();
            var newTrap = new TrapCell(pos);
            _gameArea.AddCell(newTrap);
        }

        private void UpdateTrapCount()
        {
            while (_gameArea.SelectIf<TrapCell>().Count() < Configuration.TrapCount)
                AddTrap();
        }
    }
}