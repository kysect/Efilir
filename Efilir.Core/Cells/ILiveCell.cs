using Efilir.Core.Algorithms;
using Efilir.Core.Types;

namespace Efilir.Core.Cells
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