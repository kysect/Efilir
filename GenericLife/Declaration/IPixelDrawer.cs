using System.Collections.Generic;
using GenericLife.Declaration.Cells;

namespace GenericLife.Declaration
{
    public interface IPixelDrawer
    {
        void DrawPoints(IEnumerable<IBaseCell> cells);
        void ClearBlack();
    }
}