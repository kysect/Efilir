using System.Collections.Generic;
using Efilir.Core.Generics.Cells;
using Efilir.Core.Generics.Environment;

namespace Efilir.Core.Generics.Algorithms
{
    public interface ICellBrain
    {
        List<int> CommandList { get; }
        void MakeTurn(IGenericCell cell, IGenericGameArea gameArea);
    }
}