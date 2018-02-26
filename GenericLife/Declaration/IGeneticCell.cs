using GenericLife.Types;

namespace GenericLife.Declaration
{
    public interface IGeneticCell : IBaseCell
    {
        int Health { get; set; }
        int Age { get; set; }
        int Generation { get; set; }
        int Breed { get; set; }
        ICellBrain Brain { get; set; }
        AngleRotation CurrentRotate { get; set; }

        void TurnAction();
        bool IsAlive();

        void MoveCommand(int commandRotate);
        void ActionCommand(int commandRotate);
        FieldPosition AnalyzePosition(int commandRotate);
    }
}