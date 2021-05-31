using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Efilir.Client.Tools;
using Efilir.Core.Generics.Algorithms;
using Efilir.Core.Generics.Cells;
using Efilir.Core.Generics.Environment;
using Efilir.Core.Generics.Tools;

namespace Efilir.Client.ExecutionContexts
{
    public class GenericExecutionContext : IExecutionContext
    {
        private bool _isActive;
        private readonly PixelDrawer _pd;
        private readonly ICellStatConsumer _cellStatConsumer;
        private readonly LivingCellSimulationManger _simulationManger;

        public GenericExecutionContext(PixelDrawer pixelDrawer, ICellStatConsumer cellStatConsumer)
        {
            _cellStatConsumer = cellStatConsumer;
            _pd = pixelDrawer;

            int[,] field = DataSaver.LoadField();

            _simulationManger = new LivingCellSimulationManger();
            _simulationManger.GenerateGameField(field);
            ReloadCells();

            _isActive = false;
        }

        public void SetActivity(bool isActive)
        {
            _isActive = isActive;
        }

        public void StartSimulator()
        {
            if (!_isActive)
                return;

            if (_simulationManger.IsSimulationActive())
            {
                DataSaver.Save(_simulationManger.GetAllGenericCells());
                ReloadCells();
            }

            while (!_simulationManger.IsSimulationActive())
            {
                if (!_isActive)
                    return;

                _simulationManger.ProcessIteration();
                //TODO: Fix
                Application.Current.Dispatcher.Invoke(() => _pd.DrawPoints(_simulationManger.GetAllCells()));
            }

            IReadOnlyCollection<IGenericCell> cellList = GetCellRating();
            _cellStatConsumer.NotifyStatUpdate(cellList);
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
    }
}