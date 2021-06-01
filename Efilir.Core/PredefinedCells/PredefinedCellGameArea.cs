using System.Collections.Generic;
using System.Linq;
using Efilir.Core.Environment;
using Efilir.Core.PredefinedCells.Cells;
using Efilir.Core.Types;

namespace Efilir.Core.PredefinedCells
{
    public class PredefinedCellGameArea : GameArea
    {
        public List<Vector> PreviousCellPosition { get; private set; }
        public List<PredefinedCell> PredefinedCells { get; }

        public PredefinedCellGameArea(int areaSize) : base(areaSize)
        {
            PreviousCellPosition = new List<Vector>();
            PredefinedCells = new List<PredefinedCell>();
        }

        public void UpdatePreviousPositions()
        {
            PreviousCellPosition = PredefinedCells.Select(c => c.RealPosition).ToList();
        }
    }
}