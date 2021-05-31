using Efilir.Core.Cells;

namespace Efilir.Core.Environment
{
    public interface IDrawableSimulation
    {
        IBaseCell[,] GetAllCells();
        void ProcessIteration();
        bool IsSimulationActive();
    }
}