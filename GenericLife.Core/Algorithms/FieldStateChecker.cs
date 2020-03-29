using GenericLife.Core.Cells;
using GenericLife.Core.Types;

namespace GenericLife.Core.Algorithms
{
    public interface IGameArea
    {
        IBaseCell GetCellOnPosition(Coordinate position);
        void TryEat(IGenericCell sender, Coordinate foodPosition);
        bool TryCreateCellChild(IGenericCell cell);
    }
}