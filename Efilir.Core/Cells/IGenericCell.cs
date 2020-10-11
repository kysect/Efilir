using Efilir.Core.Algorithms;
using Efilir.Core.Types;

namespace Efilir.Core.Cells
{
    public interface IGenericCell : ILiveCell
    {
        void MoveCommand(int commandRotate, IGameArea gameArea);
        void ActionCommand(int commandRotate, IGameArea gameArea);
        Coordinate AnalyzePosition(int commandRotate);
    }
}       