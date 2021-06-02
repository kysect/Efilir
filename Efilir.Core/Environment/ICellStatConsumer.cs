using System.Collections.Generic;
using Efilir.Core.Generics.Cells;

namespace Efilir.Core.Environment
{
    public interface ICellStatConsumer
    {
        void NotifyStatUpdate(IReadOnlyCollection<IGenericCell> cellStatistic);
    }
}