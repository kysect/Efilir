using System.Collections.Generic;
using GenericLife.Core.CellAbstractions;

namespace GenericLife.Core
{
    public interface ICellBrain
    {
        List<int> CommandList { get; }
        void MakeTurn(IGenericCell cell);
    }
}