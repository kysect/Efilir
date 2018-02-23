using System.Collections.Generic;

namespace GenericLife.Declaration
{
    //TODO: Generic cell type?
    public interface ICellBrain
    {
        //TODO: Walls
        IGeneticCell Cell { get; set; }
        List<int> CommandList { get; set; }
        void MakeTurn();
        ICellBrain GenerateChild();
        ICellBrain GenerateChildWithMutant();
    }
}