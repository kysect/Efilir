using GenericLife.Core.Algorithms;
using GenericLife.Core.Types;

namespace GenericLife.Core.Cells
{
    public interface ILiveCell : IBaseCell
    {
        int Health { get; set; }
        int Age { get; }
        ICellBrain Brain { get; }
        AngleRotation CurrentRotate { get; set; }

        bool IsAlive();
    }
}