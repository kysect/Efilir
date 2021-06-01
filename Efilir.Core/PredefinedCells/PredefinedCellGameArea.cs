using System.Collections.Generic;
using Efilir.Core.Environment;
using Efilir.Core.PredefinedCells.Cells;

namespace Efilir.Core.PredefinedCells
{
    public class PredefinedCellGameArea : GameArea
    {
        public List<PredefinedCell> PredefinedCells { get; }

        public PredefinedCellGameArea(int areaSize) : base(areaSize)
        {
            PredefinedCells = new List<PredefinedCell>();
        }
    }
}