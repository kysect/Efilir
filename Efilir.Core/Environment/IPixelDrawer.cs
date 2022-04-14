using System.Collections.Generic;
using Efilir.Core.Cells;

namespace Efilir.Core.Environment
{
    public interface IPixelDrawer
    {
        void DrawPoints(IBaseCell[,] cells);
        void DrawPoints<T>(ICollection<T> cells) where T : IBaseCell;
    }
}