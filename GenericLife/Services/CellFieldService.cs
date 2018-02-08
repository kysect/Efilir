using System;
using System.Collections.Generic;
using System.Linq;
using GenericLife.Models;

namespace GenericLife.Services
{
    public class CellFieldService
    {
        public readonly List<SimpleCell> Cells;
        public readonly List<FoodCell> Foods;
        public readonly int Height;
        public readonly int Width;

        private readonly Random _rand;

        public CellFieldService()
        {
            Cells = new List<SimpleCell>();
            Foods = new List<FoodCell>();
            _rand = new Random();
            Width = 100;
            Height = 100;
        }

        public PointType GetPointType(int positionX, int positionY)
        {
            if (positionX < 0 || positionX >= 100 || positionY < 0 || positionY >= 100)
                return PointType.OutOfRange;

            var isFood = Foods.Any(c => c.PositionX == positionX && c.PositionY == positionY);
            if (isFood) return PointType.Food;
            var isPoint = Cells.Any(c => c.PositionX == positionX && c.PositionY == positionY);
            return isPoint ? PointType.Cell : PointType.Void;
        }

        public void RandomMove()
        {
            foreach (var cell in Cells)
            {
                cell.RandomMove();
            }
        }
    }
}