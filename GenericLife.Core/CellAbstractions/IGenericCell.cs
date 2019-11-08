using GenericLife.Core.Types;

namespace GenericLife.Core.CellAbstractions
{
    public interface IGenericCell : ILiveCell
    {
        void MoveCommand(int commandRotate);
        void ActionCommand(int commandRotate);
        Coordinate AnalyzePosition(int commandRotate);
    }
}       