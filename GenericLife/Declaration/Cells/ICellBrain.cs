using System.Collections.Generic;
using GenericLife.Models.Cells;

namespace GenericLife.Declaration.Cells
{
    //TODO: Generic cell type?
    public interface ICellBrain
    {
        //TODO: Walls
        GenericCell Cell { get; set; }
        List<int> CommandList { get; set; }
        void MakeTurn();
        ICellBrain GenerateChild();
        ICellBrain GenerateChildWithMutant();
    }
}