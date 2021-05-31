using Efilir.Core.Generics.Algorithms;
using Efilir.Core.Generics.Environment;
using Efilir.Core.Types;

namespace Efilir.Core.Cells
{
    public class WallCell : IBaseCell
    {
        public Coordinate Position { get; set; }
        public void MakeTurn(IGenericGameArea gameArea)
        {
        }
    }
}