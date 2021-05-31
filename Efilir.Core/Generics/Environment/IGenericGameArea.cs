using Efilir.Core.Cells;
using Efilir.Core.Generics.Cells;
using Efilir.Core.Types;

namespace Efilir.Core.Generics.Environment
{
    public interface IGenericGameArea
    {
        IBaseCell GetCellOnPosition(Coordinate position);
        void TryEat(IGenericCell sender, Coordinate foodPosition);
        bool TryCreateCellChild(IGenericCell cell);
    }
}