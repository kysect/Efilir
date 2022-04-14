using System.Collections.Generic;
using System.Linq;
using Efilir.Core.Environment;
using Efilir.Core.PredefinedCells.Cells;
using Efilir.Core.Tools;
using Efilir.Core.Types;

namespace Efilir.Core.PredefinedCells
{
    public class PredefinedCellGameArea : GameArea
    {

        public LinkedList<List<(PredefinedCellType, Vector)>> PreviousSteps { get; private set; }
        public List<(PredefinedCellType, Vector)> PreviousCellPosition { get; private set; }
        public List<BasePredefinedCell> PredefinedCells { get; }

        public PredefinedCellGameArea(int areaSize) : base(areaSize)
        {
            PreviousSteps = new LinkedList<List<(PredefinedCellType, Vector)>>();
            PreviousCellPosition = new List<(PredefinedCellType, Vector)>();
            PredefinedCells = new List<BasePredefinedCell>();
        }

        public void UpdatePreviousPositions()
        {
            while (PreviousSteps.Count >= Configuration.PreviousStepCount)
            {
                PreviousSteps.RemoveFirst();
            }

            PreviousCellPosition = PredefinedCells.Select(c => (c.CellType, c.RealPosition)).ToList();
            PreviousSteps.AddLast(PreviousCellPosition);
        }
    }
}