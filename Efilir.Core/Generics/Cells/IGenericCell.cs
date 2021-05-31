using Efilir.Core.Generics.Algorithms;
using Efilir.Core.Types;

namespace Efilir.Core.Generics.Cells
{
    public interface IGenericCell : ILiveCell
    {
        void MoveCommand(int commandRotate, IGameArea gameArea);
        void ActionCommand(int commandRotate, IGameArea gameArea);
        Coordinate AnalyzePosition(int commandRotate);
    }
}       