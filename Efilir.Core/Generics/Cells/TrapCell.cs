using Efilir.Core.Cells;
using Efilir.Core.Generics.Algorithms;
using Efilir.Core.Generics.Environment;
using Efilir.Core.Types;

namespace Efilir.Core.Generics.Cells
{
    public class TrapCell : IBaseCell
    {
        public Coordinate Position { get; set; }

        public TrapCell(Coordinate position)
        {
            Position = position;
        }

        public void MakeTurn(IGenericGameArea gameArea)
        {
            
        }
    }
}