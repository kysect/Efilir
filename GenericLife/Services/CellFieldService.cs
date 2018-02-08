using System;
using System.Collections.Generic;
using System.Linq;
using GenericLife.Models;
using GenericLife.Tools;

namespace GenericLife.Services
{
    public class CellFieldService
    {
        public readonly List<SimpleCell> Cells;
        public readonly List<FoodCell> Foods;
        public readonly int Height;
        public readonly int Width;
        public readonly int FieldSize = 100;
        private readonly Random _rand;

        public CellFieldService()
        {
            Cells = new List<SimpleCell>();
            Foods = new List<FoodCell>();
            _rand = new Random();
            Width = FieldSize;
            Height = FieldSize;
        }

        public void AddRandomCell()
        {
            int x, y;
            do
            {
                x = GlobalRand.Next(FieldSize);
                y = GlobalRand.Next(FieldSize);
            } while (GetPointType(x, y) != PointType.Void);
            Cells.Add(new SimpleCell(this, x, y));
        }

        public void AddFood()
        {
            int x, y;
            do
            {
                x = GlobalRand.Next(FieldSize);
                y = GlobalRand.Next(FieldSize);
            } while (GetPointType(x, y) != PointType.Void);
            Foods.Add(new FoodCell(x, y));
        }

        public PointType GetPointType(int positionX, int positionY)
        {
            if (positionX < 0 || positionX >= FieldSize || positionY < 0 || positionY >= FieldSize)
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