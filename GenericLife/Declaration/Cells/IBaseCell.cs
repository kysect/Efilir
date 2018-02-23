using System;
using System.Windows.Media;
using GenericLife.Declaration.Cells;
using GenericLife.Models;
using GenericLife.Models.Cells;
using GenericLife.Types;

namespace GenericLife.Declaration.Cells
{
    public interface IBaseCell
    {
        FieldPosition Position { get; set; }
        ICellField FieldModel { get; set; }
    }

    public static class Extension
    {
        public static PointType GetPointType(this IBaseCell cell)
        {
            switch (cell)
            {
                case FoodCell _:
                    return PointType.Food;
                case WallCell _:
                    return PointType.Wall;
                case ILiveCell lc:
                    return PointType.Cell;
                default:
                    throw new ArgumentException();
            }
        }

    }
}