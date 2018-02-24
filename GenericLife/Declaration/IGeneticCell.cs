using GenericLife.Types;

namespace GenericLife.Declaration
{
    public interface IGeneticCell : ILiveCell
    {
        int Generation { get; set; }
        int Breed { get; set; }
        ICellBrain Brain { get; set; }
        AngleRotation CurrentRotate { get; set; }

        void MoveCommand(int commandRotate);
        void ActionCommand(int commandRotate);
        FieldPosition AnalyzePosition(int commandRotate);
    }
}