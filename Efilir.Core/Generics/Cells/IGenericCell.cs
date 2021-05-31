using Efilir.Core.Generics.Environment;
using Efilir.Core.Types;

namespace Efilir.Core.Generics.Cells
{
    public interface IGenericCell : ILiveCell
    {
        void MoveCommand(int commandRotate, IGenericGameArea gameArea);
        void ActionCommand(int commandRotate, IGenericGameArea gameArea);
        Coordinate AnalyzePosition(int commandRotate);
    }
}       