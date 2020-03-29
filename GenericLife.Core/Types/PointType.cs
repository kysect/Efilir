using System;
using GenericLife.Core.Cells;

namespace GenericLife.Core.Types
{
    public enum PointType
    {
        Void = 0,
        Cell = 1,
        //TODO: probably, can be safety removed
        DeadCell = 2,
        Wall = 3,
        Food = 4
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
                case IGenericCell lc:
                    return lc.IsAlive() ? PointType.Cell : PointType.DeadCell;
                default:
                    throw new ArgumentException();
            }
        }
    }
}