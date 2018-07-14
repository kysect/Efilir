using GenericLife.Types;

namespace GenericLife.Interfaces
{
    public interface ILiveCell : IBaseCell
    {
        int Health { get; }
        int Age { get; }
        ICellBrain Brain { get; }
        AngleRotation CurrentRotate { get; set; }

        bool IsAlive();
    }
}