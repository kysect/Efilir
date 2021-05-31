using System.Collections.Generic;
using System.Windows.Controls;
using Efilir.Core.Algorithms;
using Efilir.Core.Cells;
using Efilir.Core.Environment;
using Efilir.Core.Tools;

namespace Efilir.Client.Tools
{
    public class TestingPolygon
    {
        private readonly PixelDrawer _pd;
        public readonly LivingCellSimulationManger SimulationManger;

        public TestingPolygon(Image image)
        {
            _pd = new PixelDrawer(image, Configuration.FieldSize, Configuration.ScaleSize);
            int[,] field = DataSaver.LoadField();

            SimulationManger = new LivingCellSimulationManger();
            SimulationManger.GenerateGameField(field);
        }

        public void UpdateUi() => _pd.DrawPoints(SimulationManger.GetAllCells());
        public void SimulateRound() => SimulationManger.ProcessIteration();
        public bool SimulationFinished => SimulationManger.IsSimulationActive();
        public void SaveCells() => DataSaver.Save(SimulationManger.GetAllGenericCells());

        public void LoadCells()
        {
            List<List<int>> jsonData = DataSaver.Load();
            List<IGenericCell> generatedCells = GeneticCellMutation.GenerateNewCells(jsonData);
            var field = DataSaver.LoadField();

            SimulationManger.DeleteAllElements();
            SimulationManger.GenerateGameField(field);

            SimulationManger.InitializeLiveCells(generatedCells);
        }
    }
}