using GenericLife.Core.Algorithms;
using GenericLife.Core.Types;

namespace GenericLife.Core.Cells
{
    public interface IBaseCell
    {
        Coordinate Position { get; set; }
        void MakeTurn(IGameArea gameArea);
    }
}