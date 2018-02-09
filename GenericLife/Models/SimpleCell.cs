﻿using System.Linq;
using System.Windows.Media;
using GenericLife.Declaration;
using GenericLife.Tools;

namespace GenericLife.Models
{
    public class SimpleCell : ILiveCell
    {
        private readonly ICellField _fieldService;

        public SimpleCell(ICellField fieldService, int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
            _fieldService = fieldService;

            Health = 100;
        }

        public int Health { get; set; }

        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Age { get; set; }

        public void TurnAction()
        {
            RandomMove();
        }

        public Color GetColor()
        {
            if (Health == 0)
                return CellColorGenerator.DeadCell();
            return CellColorGenerator.HealthIndicator(Health);
        }

        public void RandomMove()
        {
            if (Health == 0)
                return;

            int x, y;
            do
            {
                x = GlobalRand.Next(3) - 1;
                y = GlobalRand.Next(3) - 1;
            } while (x == 0 && y == 0);

            Move(PositionX + x, PositionY + y);
        }

        private void Move(int positionX, int positionY)
        {
            var cellType = _fieldService.GetPointType(positionX, positionY);
            Health -= 1;
            Age += 1;
            if (cellType == PointType.Food)
            {
                var cellOnWay = _fieldService.GetCellOnPosition(positionX, positionY);
                _fieldService.Foods.Remove(cellOnWay as FoodCell);
                PositionX = positionX;
                PositionY = positionY;
                Health += FoodCell.FoodHealthIncome;

                return;
            }

            if (cellType == PointType.Void)
            {
                PositionX = positionX;
                PositionY = positionY;
            }
        }
    }
}