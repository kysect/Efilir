﻿using System;
using System.Windows.Media;
using Efilir.Core.Cells;
using Efilir.Core.Generics.Cells;

namespace Efilir.Client.Tools
{
    public static class CellColorGenerator
    {
        public static Color GetCellColor(IBaseCell cell)
        {
            switch (cell)
            {
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
    }
}