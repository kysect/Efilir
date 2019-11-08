using GenericLife.Core.Types;

namespace GenericLife.Core.CellAbstractions
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