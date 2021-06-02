using System.Collections.Generic;
using System.Linq;
using Efilir.Core.Environment;
using Efilir.Core.Types;

namespace Efilir.Core.PredefinedCells
{
    public class PredefinedCellGameArea : GameArea
    {
        public List<(PredefinedCellType, Vector)> PreviousCellPosition { get; private set; }
        public List<PredefinedCell> PredefinedCells { get; }

        public PredefinedCellGameArea(int areaSize) : base(areaSize)
        {
            PreviousCellPosition = new List<(PredefinedCellType, Vector)>();
            PredefinedCells = new List<PredefinedCell>();
        }

        public void UpdatePreviousPositions()
        {
            PreviousCellPosition = PredefinedCells.Select(c => (c.CellType, c.RealPosition)).ToList();
        }
    }
}