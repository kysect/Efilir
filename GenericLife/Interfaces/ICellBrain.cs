using System.Collections.Generic;

namespace GenericLife.Interfaces
{
    public interface ICellBrain
    {
        List<int> CommandList { get; }
        void MakeTurn();
    }
}