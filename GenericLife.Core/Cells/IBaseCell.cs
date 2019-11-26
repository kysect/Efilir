using GenericLife.Core.Environment;
using GenericLife.Core.Types;

namespace GenericLife.Core.Cells
{
    public interface IBaseCell
    {
        Coordinate Position { get; set; }
        GameArea Field { get; set; }
        void MakeTurn();
    }
}