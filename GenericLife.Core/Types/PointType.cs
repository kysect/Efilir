using System;
using GenericLife.Core.CellAbstractions;
using GenericLife.Core.Models.Cells;

namespace GenericLife.Core.Types
{
    public enum PointType
    {
        Void,
        Cell,
        DeadCell,
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
                case IGenericCell lc:
                    return lc.IsAlive() ? PointType.Cell : PointType.DeadCell;
                default:
                    throw new ArgumentException();
            }
        }
    }
}