using System.Collections.Generic;

namespace GenericLife.Declaration
{
    public interface ICellBrain
    {
        IGeneticCell Cell { get; set; }
        List<int> CommandList { get; set; }
        void MakeTurn();
        ICellBrain GenerateChild();
        ICellBrain GenerateChildWithMutant();
    }
}