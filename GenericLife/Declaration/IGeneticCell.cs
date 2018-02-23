using GenericLife.Types;

namespace GenericLife.Declaration
{
    public interface IGeneticCell : ILiveCell
    {
        ICellBrain Brain { get; set; }
        AngleRotation CurrentRotate { get; set; }

        void MoveCommand(int commandRotate);
        void ActionCommand(int commandRotate);
        FieldPosition GetTargetPosition(int commandRotate);
    }
}