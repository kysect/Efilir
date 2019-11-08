using System;
using System.Windows.Media;
using GenericLife.Core.CellAbstractions;
using GenericLife.Core.Models.Cells;

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
                case IGenericCell lc:
                    return HealthIndicator(lc.Health);
                case WallCell _:
                    return WallColor();
                default:
                    throw new ArgumentException();
            }
        }

        public static Color DeadCell()
        {
            return Colors.IndianRed;
        }

        public static Color HealthIndicator(int health)
        {
            if (health <= 0) return DeadCell();
            return Colors.AliceBlue;
        }

        public static Color FoodColor()
        {
            return Colors.GreenYellow;
        }

        public static Color WallColor()
        {
            return Colors.Gold;
        }
    }
}