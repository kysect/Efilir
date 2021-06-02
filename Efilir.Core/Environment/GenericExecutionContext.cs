using System.Collections.Generic;
using System.Linq;
using Efilir.Core.Environment;
using Efilir.Core.Generics.Algorithms;
using Efilir.Core.Generics.Cells;
using Efilir.Core.Generics.Environment;
using Efilir.Core.Generics.Tools;

namespace Efilir.Client.ExecutionContexts
{
    public class GenericExecutionContext : IExecutionContext
    {
        private readonly IPixelDrawer _pd;
        private readonly ICellStatConsumer _cellStatConsumer;
        private readonly LivingCellSimulationManger _simulationManger;

        public GenericExecutionContext(IPixelDrawer pixelDrawer, ICellStatConsumer cellStatConsumer)
        {
            _cellStatConsumer = cellStatConsumer;
            _pd = pixelDrawer;

            int[,] field = DataSaver.LoadField();

            _simulationManger = new LivingCellSimulationManger();
            _simulationManger.GenerateGameField(field);
            ReloadCells();
        }

        private void ReloadCells()
        {
            List<List<int>> jsonData = DataSaver.Load();
            List<IGenericCell> generatedCells = GeneticCellMutation.GenerateNewCells(jsonData);
            int[,] field = DataSaver.LoadField();

            _simulationManger.DeleteAllElements();
            _simulationManger.GenerateGameField(field);
            _simulationManger.InitializeLiveCells(generatedCells);
        }

        private IReadOnlyCollection<IGenericCell> GetCellRating()
        {
            IReadOnlyCollection<IGenericCell> cellList = _simulationManger.GetAllGenericCells();

            List<IGenericCell> orderByDescending = cellList
                .OrderByDescending(c => c.Age)
                .ThenByDescending(c => c.Health)
                .ToList();

            return orderByDescending;
        }

        public void OnRoundStart()
        {
        }

        public bool OnIterationStart()
        {
            _simulationManger.ProcessIteration();
            return _simulationManger.IsSimulationActive();
        }

        public void OnRoundEnd()
        {
            IReadOnlyCollection<IGenericCell> cellList = GetCellRating();
            _cellStatConsumer.NotifyStatUpdate(cellList);
        }

        public void OnUiRender()
        {
            _pd.DrawPoints(_simulationManger.GetAllCells());
        }
    }
}