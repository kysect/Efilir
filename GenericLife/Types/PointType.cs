using System;
using GenericLife.Declaration;
using GenericLife.Models.Cells;

namespace GenericLife.Types
{
    public enum PointType
    {
        Void,
        Cell,
        Wall,
        Food
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