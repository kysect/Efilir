using GenericLife.Core.Algorithms;
using GenericLife.Core.Types;

namespace GenericLife.Core.Cells
{
    public interface IGenericCell : ILiveCell
    {
        void MoveCommand(int commandRotate, IGameArea gameArea);
        void ActionCommand(int commandRotate, IGameArea gameArea);
        Coordinate AnalyzePosition(int commandRotate);
    }
}       