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
                    return Colors.GreenYellow;
                case IGenericCell lc when lc.Health <= 0:
                    return Colors.IndianRed;
                case IGenericCell _:
                    return Colors.AliceBlue;
                case WallCell _:
                    return Colors.Gold;
                default:
                    throw new ArgumentException($"{cell.GetType()}");
            }
        }
    }
}