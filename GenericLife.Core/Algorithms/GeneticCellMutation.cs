using System.Collections.Generic;
using System.Linq;
using GenericLife.Core.Cells;
using GenericLife.Core.Tools;

namespace GenericLife.Core.Algorithms
{
    public static class GeneticCellMutation
    {
        public static List<IGenericCell> GenerateNewCells(IEnumerable<List<int>> jsonData)
        {
            var cellsList = new List<IGenericCell>();
            foreach (List<int> commandList in jsonData)
            {
                List<int> newList = commandList.Select(v => GlobalRand.Next(64)).ToList();
                cellsList.Add(new GenericCell(new CellBrain(newList)));
            }

            return cellsList;
        }
    }
}