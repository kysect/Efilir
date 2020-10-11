using System;
using System.Windows.Media;
using Efilir.Core.Cells;

namespace Efilir.Client.Tools
{
    public static class CellColorGenerator
    {
        public static Color GetCellColor(IBaseCell cell)
        {
            switch (cell)
            {
                case FoodCell _:
                    return Colors.GreenYellow;
                case TrapCell _:
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