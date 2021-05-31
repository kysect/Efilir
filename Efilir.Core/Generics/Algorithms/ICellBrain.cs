using System.Collections.Generic;
using Efilir.Core.Generics.Cells;

namespace Efilir.Core.Generics.Algorithms
{
    public interface ICellBrain
    {
        List<int> CommandList { get; }
        void MakeTurn(IGenericCell cell, IGameArea gameArea);
    }
}