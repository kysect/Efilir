using Efilir.Core.Tools;

namespace Efilir.Core.PredefinedCells
{
    public enum PredefinedCellType
    {
        Red,
        Blue,
        Green
    }

    public static class PredefinedCellTypeExtensions
    {
        public static PredefinedCellType GetAny()
        {
            return (PredefinedCellType)GlobalRand.Next(3);
        }
    }
}