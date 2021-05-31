using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Efilir.Client.Tools;
using Efilir.Core.Generics.Algorithms;
using Efilir.Core.Generics.Cells;
using Efilir.Core.Generics.Environment;
using Efilir.Core.Generics.Tools;
using Efilir.Core.Tools;

namespace Efilir.Client.ExecutionContexts
{
    public class GenericExecutionContext
    {
        private readonly PixelDrawer _pd;

        public GenericExecutionContext(Image image)
        {
            _pd = new PixelDrawer(image, Configuration.FieldSize, Configuration.ScaleSize);
            int[,] field = DataSaver.LoadField();

            SimulationManger = new LivingCellSimulationManger();
            SimulationManger.GenerateGameField(field);

            IsActive = false;
        }

        public LivingCellSimulationManger SimulationManger { get; }
        public bool IsActive { get; set; }

        public void UpdateUi() => _pd.DrawPoints(SimulationManger.GetAllCells());
        public void SimulateRound() => SimulationManger.ProcessIteration();
        public bool SimulationFinished => SimulationManger.IsSimulationActive();
        public void SaveCells() => DataSaver.Save(SimulationManger.GetAllGenericCells());

        public void StartSimulator()
        {
            if (SimulationFinished)
            {
                SaveCells();
                LoadCells();
            }

            while (!SimulationFinished)
            {
                if (!IsActive)
                    return;

                SimulateRound();
                //TODO: Fix
                Application.Current.Dispatcher.Invoke(() => UpdateUi());
            }
        }

        public void LoadCells()
        {
            List<List<int>> jsonData = DataSaver.Load();
            List<IGenericCell> generatedCells = GeneticCellMutation.GenerateNewCells(jsonData);
            int[,] field = DataSaver.LoadField();

            SimulationManger.DeleteAllElements();
            SimulationManger.GenerateGameField(field);
            SimulationManger.InitializeLiveCells(generatedCells);
        }

        public IReadOnlyCollection<IGenericCell> GetCellRating()
        {
            IReadOnlyCollection<IGenericCell> cellList = SimulationManger.GetAllGenericCells();

            List<IGenericCell> orderByDescending = cellList
                .OrderByDescending(c => c.Age)
                .ThenByDescending(c => c.Health)
                .ToList();

            return orderByDescending;
        }
    }
}