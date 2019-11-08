using System.Windows.Controls;
using GenericLife.Core.Models;
using GenericLife.Core.Tools;

namespace GenericLife.Tools
{
    public class TestingPolygon
    {
        private readonly PixelDrawer _pd;
        public readonly SimulationManger CellField;

        public TestingPolygon(PixelDrawer pd, SimulationManger cellField)
        {
            _pd = pd;
            CellField = cellField;
        }

        public TestingPolygon(Image image)
        {
            _pd = new PixelDrawer(image);
            CellField = new SimulationManger();
        }


        public void UpdateUi()
        {
            _pd.DrawPoints(CellField.GetAllCells());
        }

        public void RandomMove()
        {
            CellField.MakeCellsMove();
        }

        public void SaveCells()
        {
            var cells = CellField.GetAllGenericCells();
            JsonSaver.Save(cells);
        }

        public void LoadCells()
        {
            var jsonData = JsonSaver.Load();
            var generatedCells = GeneticCellMutation.GenerateNewCells(jsonData);

            CellField.DeleteAllElements();
            CellField.InitializeLiveCells(generatedCells);
        }
    }
}