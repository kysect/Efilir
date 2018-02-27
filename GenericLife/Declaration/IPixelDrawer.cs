using System.Collections.Generic;

namespace GenericLife.Declaration
{
    public interface IPixelDrawer
    {
        void DrawPoints(IEnumerable<IBaseCell> cells);
        void DrawPoints(IBaseCell[,] cells);
        void ClearBlack();
    }
}