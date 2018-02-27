using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using GenericLife.Declaration;
using GenericLife.Tools;

namespace GenericLife.Models
{
    public class TestingPolygon
    {
        private readonly IPixelDrawer _pd;
        public readonly ICellField CellField;

        public TestingPolygon(IPixelDrawer pd, ICellField cellField)
        {
            _pd = pd;
            CellField = cellField;
        }

        public TestingPolygon(Image image)
        {
            _pd = new PixelDrawer(image);
            CellField = new CellFieldModel();
        }


        public void UpdateUi()
        {
            _pd.ClearBlack();
            _pd.DrawPoints(CellField.CellsPosition);
        }
        public void RandomMove()
        {
            CellField.MakeCellsMove();
        }

        public void SaveCells()
        {
            var cells = CellField.GetAllLiveCells().Select(c => c);
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