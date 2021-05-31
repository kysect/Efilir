using Efilir.Core.Generics.Algorithms;
using Efilir.Core.Generics.Environment;
using Efilir.Core.Types;

namespace Efilir.Core.Cells
{
    public interface IBaseCell
    {
        Coordinate Position { get; set; }
        void MakeTurn(IGenericGameArea gameArea);
    }
}