using GenericLife.Types;

namespace GenericLife.Interfaces
{
    public interface IGenericCell : IBaseCell
    {
        int Health { get; }
        int Age { get; }

        //TODO: implement
        int Generation { get; set; }
        int Breed { get; set; }
        ICellBrain Brain { get; }
        AngleRotation CurrentRotate { get; set; }

        void TurnAction();
        bool IsAlive();

        void MoveCommand(int commandRotate);
        void ActionCommand(int commandRotate);
        Coordinate AnalyzePosition(int commandRotate);
    }
}