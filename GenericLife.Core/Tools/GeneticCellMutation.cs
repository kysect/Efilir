using System.Collections.Generic;
using GenericLife.Core.CellAbstractions;
using GenericLife.Core.Models.Cells;

namespace GenericLife.Core.Tools
{
    public static class GeneticCellMutation
    {
        public static List<IGenericCell> GenerateNewCells(IEnumerable<List<int>> jsonData)
        {
            var cellsList = new List<IGenericCell>();
            foreach (var commandList in jsonData)
            {

                for (var i = 0; i < 6; i++)
                {
                    cellsList.Add(new GenericCell(new CellBrain(commandList)));
                }

                for (var i = 0; i < 2; i++)
                {
                    var list = new List<int>(commandList);
                    var index = GlobalRand.Next(list.Count);
                    list[index] = GlobalRand.Next(64);

                    cellsList.Add(new GenericCell(new CellBrain(list)));
                }
            }

            return cellsList;
        }
    }
}