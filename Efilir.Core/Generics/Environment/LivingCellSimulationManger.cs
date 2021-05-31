using System.Collections.Generic;
using System.Linq;
using Efilir.Core.Cells;
using Efilir.Core.Environment;
using Efilir.Core.Generics.Cells;
using Efilir.Core.Tools;
using Efilir.Core.Types;

namespace Efilir.Core.Generics.Environment
{
    public class LivingCellSimulationManger : IDrawableSimulation
    {
        private const int FoodCount = Configuration.FoodCount;
        private readonly GenericGameArea _genericGameArea;

        public LivingCellSimulationManger()
        {
            _genericGameArea = new GenericGameArea(Configuration.FieldSize);
        }

        public IReadOnlyCollection<IGenericCell> GetAllGenericCells()
        {
            return _genericGameArea.Cells.OfType<IGenericCell>().ToList();
        }

        public void DeleteAllElements()
        {
            _genericGameArea.CleanField();
            _genericGameArea.GenerateRandomWall();
        }

        public void GenerateGameField(int[,] cells = null)
        {
            if (cells is null)
                _genericGameArea.GenerateRandomWall();
            else
                _genericGameArea.GenerateGameField(cells);
        }

        public IBaseCell[,] GetAllCells()
        {
            return _genericGameArea.Cells;
        }

        public void ProcessIteration()
        {
            UpdateFoodCount();
            UpdateTrapCount();
            foreach (IBaseCell cell in _genericGameArea.Cells.OfType<IBaseCell>())
            {
                Coordinate oldPosition = cell.Position;
                cell.MakeTurn(_genericGameArea);
                if (oldPosition != cell.Position)
                {
                    _genericGameArea.RemoveCell(oldPosition);
                    _genericGameArea.AddCell(cell);
                }
            }

            List<IGenericCell> deadCellList = _genericGameArea.Cells.OfType<IGenericCell>().Where(c => !c.IsAlive()).ToList();
            foreach (IGenericCell deadCell in deadCellList)
            {
                _genericGameArea.AddCell(new FoodCell(deadCell.Position));
            }
        }

        public void InitializeLiveCells(IEnumerable<IGenericCell> cellsList)
        {
            foreach (IGenericCell liveCell in cellsList)
            {
                liveCell.Position = _genericGameArea.GetEmptyPosition();
                _genericGameArea.AddCell(liveCell);
            }
        }

        public bool IsSimulationActive()
        {
            //TODO: move as argument
            return _genericGameArea.Cells.OfType<IGenericCell>().Count(c => c.IsAlive()) <= 8;
        }

        private void AddFood()
        {
            Coordinate pos = _genericGameArea.GetEmptyPosition();
            var newFood = new FoodCell(pos);
            _genericGameArea.AddCell(newFood);
        }

        private void UpdateFoodCount()
        {
            while (_genericGameArea.Cells.OfType<FoodCell>().Count() < FoodCount)
                AddFood();
        }

        private void AddTrap()
        {
            Coordinate pos = _genericGameArea.GetEmptyPosition();
            var newTrap = new TrapCell(pos);
            _genericGameArea.AddCell(newTrap);
        }

        private void UpdateTrapCount()
        {
            while (_genericGameArea.Cells.OfType<TrapCell>().Count() < Configuration.TrapCount)
                AddTrap();
        }
    }
}