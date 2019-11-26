using GenericLife.Core.Algorithms;
using GenericLife.Core.Types;

namespace GenericLife.Core.Cells
{
    public class WallCell : IBaseCell
    {
        public Coordinate Position { get; set; }
        public void MakeTurn(IGameArea gameArea)
        {
        }
    }
}