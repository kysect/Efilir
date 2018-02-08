using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using GenericLife.Services;
using GenericLife.Tools;

namespace GenericLife.Models
{
    public class SimpleCell : IBaseCell
    {
        private readonly CellFieldService _fieldService;

        public SimpleCell(CellFieldService fieldService, int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
            _fieldService = fieldService;

            HitPoint = 100;
        }

        public SimpleCell(CellFieldService fieldService, int positionX, int positionY, byte hitpoint)
        {
            PositionX = positionX;
            PositionY = positionY;
            _fieldService = fieldService;

            HitPoint = hitpoint;
        }

        public int HitPoint { get; set; }

        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public Color GetColor()
        {
            if (HitPoint == 0)
            {
                return new Color
                {
                    R = Byte.MaxValue,
                    G = Byte.MaxValue,
                    B = Byte.MaxValue,
                };
            }
            return new Color
            {
                G = (byte) (HitPoint * 2),
                R = (byte) (200 - HitPoint * 2),
                B = 0
            };
        }

        public void RandomMove()
        {
            if (HitPoint == 0)
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
            if (cellType == PointType.Food)
            {
                var forCleaning = _fieldService.Foods.First(c => c.PositionY == positionY && c.PositionX == positionX);
                _fieldService.Foods.Remove(forCleaning);
                PositionX = positionX;
                PositionY = positionY;
                HitPoint += FoodCell.FoodHealthIncome;
                
                return;
            }

            if (cellType == PointType.Void)
            {
                PositionX = positionX;
                PositionY = positionY;
            }

            HitPoint -= 1;
        }
    }
}