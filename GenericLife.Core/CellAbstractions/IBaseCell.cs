using GenericLife.Core.Models;
using GenericLife.Core.Types;

namespace GenericLife.Core.CellAbstractions
{
    public interface IBaseCell
    {
        Coordinate Position { get; set; }
        GameArea Field { get; set; }
        void MakeTurn();
    }
}