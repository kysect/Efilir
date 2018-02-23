using System;
using System.Windows.Media;
using GenericLife.Declaration.Cells;
using GenericLife.Models.Cells;

namespace GenericLife.Tools
{
    public static class CellColorGenerator
    {
        public static Color GetCellColor(IBaseCell cell)
        {
            switch (cell)
            {
                case FoodCell _:
                    return FoodColor();
                case ILiveCell lc:
                    return HealthIndicator(lc.Health);
                case WallCell _:
                    return WallColor();
                default:
                    throw new ArgumentException();
            }
        }

        public static Color DeadCell()
        {
            //return Colors.Maroon;
            return new Color
            {
                R = byte.MaxValue,
                G = byte.MaxValue,
                B = byte.MaxValue
            };
        }

        public static Color HealthIndicator(int health)
        {
            if (health <= 0) return DeadCell();

            health = Math.Min(health, 100);
            return new Color
            {
                R = (byte) (200 - health * 2),
                G = (byte) (health * 2),
                B = 0
            };
        }

        public static Color FoodColor()
        {
            //return Colors.AliceBlue;
            return new Color
            {
                R = 0,
                G = 0,
                B = 200
            };
        }

        public static Color WallColor()
        {
            return Colors.Azure;
        }
    }
}