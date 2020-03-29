﻿using System.Collections.Generic;
using System.Linq;
using GenericLife.Core.Cells;
using GenericLife.Core.Tools;
using GenericLife.Core.Types;

namespace GenericLife.Core.Environment
{
    public class SimulationManger
    {
        private const int FoodCount = Configuration.FoodCount;
        private readonly GameArea _gameArea;

        public SimulationManger()
        {
            _gameArea = new GameArea(Configuration.FieldSize);
            DeleteAllElements();
        }

        public List<IGenericCell> GetAllGenericCells()
        {
            return _gameArea.GenericCells;
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
            foreach (var cell in _gameArea.SelectIf<IBaseCell>())
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

        public int GetAliveCellCount()
        {
            return _gameArea.GenericCells.Count(c => c.IsAlive());
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
    }
}