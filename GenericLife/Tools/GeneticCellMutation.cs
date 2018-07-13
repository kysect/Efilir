using System.Collections.Generic;
using GenericLife.Interfaces;
using GenericLife.Models.Cells;

namespace GenericLife.Tools
{
    public static class GeneticCellMutation
    {
        public static List<IGenericCell> GenerateNewCells(IEnumerable<List<int>> jsonData)
        {
            var cellsList = new List<IGenericCell>();
            foreach (var commandList in jsonData)
            {
                for (var i = 0; i < 6; i++)
                    cellsList.Add(new GenericCell(commandList));

                for (var i = 0; i < 2; i++)
                {
                    var list = new List<int>(commandList);
                    var index = GlobalRand.Next(list.Count);
                    list[index] = GlobalRand.Next(64);

                    cellsList.Add(new GenericCell(list));
                }
            }

            return cellsList;
        }
    }
}