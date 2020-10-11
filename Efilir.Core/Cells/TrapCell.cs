using Efilir.Core.Algorithms;
using Efilir.Core.Types;

namespace Efilir.Core.Cells
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