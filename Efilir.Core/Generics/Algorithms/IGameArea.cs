using Efilir.Core.Cells;
using Efilir.Core.Generics.Cells;
using Efilir.Core.Types;

namespace Efilir.Core.Generics.Algorithms
{
    public interface IGameArea
    {
        IBaseCell GetCellOnPosition(Coordinate position);
        void TryEat(IGenericCell sender, Coordinate foodPosition);
        bool TryCreateCellChild(IGenericCell cell);
    }
}