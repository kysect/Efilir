using System;
using System.Windows.Media;
using Efilir.Core.Cells;
using Efilir.Core.Generics.Cells;
using Efilir.Core.PredefinedCells.Cells;

namespace Efilir.Client.Tools
{
    public static class CellColorGenerator
    {
        public static Color GetCellColor(IBaseCell cell)
        {
            switch (cell)
            {
                case PredefinedCell predefinedCell:
                    return PredefinedCellColors(predefinedCell);
                case FoodCell:
                    return Colors.GreenYellow;
                case TrapCell:
                    return Colors.IndianRed;
                case IGenericCell:
                    return Colors.AliceBlue;
                case WallCell:
                    return Colors.Gold;
                default:
                    throw new ArgumentException($"{cell.GetType()}");
            }
        }

        public static Color PredefinedCellColors(PredefinedCell cell)
        {
            switch (cell.CellType)
            {
                case Core.PredefinedCells.PredefinedCellType.Red:
                    return Colors.IndianRed;
                case Core.PredefinedCells.PredefinedCellType.Blue:
                    return Colors.DodgerBlue;
                case Core.PredefinedCells.PredefinedCellType.Green:
                    return Colors.LightGreen;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}