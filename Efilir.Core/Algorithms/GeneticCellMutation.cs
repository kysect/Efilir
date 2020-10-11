using System.Collections.Generic;
using System.Linq;
using Efilir.Core.Cells;
using Efilir.Core.Tools;

namespace Efilir.Core.Algorithms
{
    public static class GeneticCellMutation
    {
        public static List<IGenericCell> GenerateNewCells(IEnumerable<List<int>> jsonData)
        {
            var cellsList = new List<IGenericCell>();
            foreach (List<int> commandList in jsonData)
            {
                List<int> newList = commandList.Select(v => GlobalRand.Next(Configuration.GenMaxValue)).ToList();
                cellsList.Add(new GenericCell(new CellBrain(newList)));
            }

            return cellsList;
        }
    }
}