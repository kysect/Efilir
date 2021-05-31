using Efilir.Core.Cells;
using Efilir.Core.Generics.Algorithms;
using Efilir.Core.Types;

namespace Efilir.Core.Generics.Cells
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