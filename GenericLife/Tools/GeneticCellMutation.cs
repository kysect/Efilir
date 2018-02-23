using System.Collections.Generic;
using GenericLife.Declaration;
using GenericLife.Models.Cells;

namespace GenericLife.Tools
{
    public static class GeneticCellMutation
    {
        public static List<ILiveCell> GenerateNewCells(IEnumerable<List<int>> jsonData)
        {
            var cellsList = new List<ILiveCell>();
            foreach (var cell in jsonData)
            {
                var brain = new CellBrain(cell);

                for (var i = 0; i < 2; i++)
                {
                    cellsList.Add(new GenericCell()
                    {
                        Brain = brain.GenerateChildWithMutant()
                    });
                }

                for (var i = 0; i < 6; i++)
                {
                    cellsList.Add(new GenericCell()
                    {
                        Brain = brain.GenerateChildWithMutant()
                    });
                }
            }
            return cellsList;
        }
    }
}