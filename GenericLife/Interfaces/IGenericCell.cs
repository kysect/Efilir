using GenericLife.Types;

namespace GenericLife.Interfaces
{
    public interface IGenericCell : ILiveCell
    {
        void MoveCommand(int commandRotate);
        void ActionCommand(int commandRotate);
        Coordinate AnalyzePosition(int commandRotate);
    }
}       