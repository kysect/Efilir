using GenericLife.Core.Algorithms;
using GenericLife.Core.Types;

namespace GenericLife.Core.Cells
{
    public class TrapCell : IBaseCell
    {
        public Coordinate Position { get; set; }

        public TrapCell(Coordinate position)
        {
            Position = position;
        }

        public void MakeTurn(IGameArea gameArea)
        {
            
        }
    }
}