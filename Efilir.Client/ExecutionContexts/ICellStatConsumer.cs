using System.Collections.Generic;
using Efilir.Core.Generics.Cells;

namespace Efilir.Client.ExecutionContexts
{
    public interface ICellStatConsumer
    {
        void NotifyStatUpdate(IReadOnlyCollection<IGenericCell> cellStatistic);
    }
}