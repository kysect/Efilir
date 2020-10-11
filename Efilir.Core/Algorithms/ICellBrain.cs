using System.Collections.Generic;
using Efilir.Core.Cells;

namespace Efilir.Core.Algorithms
{
    public interface ICellBrain
    {
        List<int> CommandList { get; }
        void MakeTurn(IGenericCell cell, IGameArea gameArea);
    }
}