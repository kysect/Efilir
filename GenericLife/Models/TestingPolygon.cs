using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Windows.Controls;
using GenericLife.Declaration;
using GenericLife.Models.Cells;
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
            _pd.DrawPoints(CellField.Cells);
            _pd.DrawPoints(CellField.Foods);
        }
        public void RandomMove()
        {
            CellField.MakeCellsMove();
        }

        public void SaveCells()
        {
            var cells = CellField.Cells.Select(c => c as IGeneticCell);
            JsonSaver.Save(cells);
        }

        public void LoadCells()
        {
            CellField.Cells = new List<ILiveCell>();
            var jsonData = JsonSaver.Load();
            var generatedCells = GeneticCellMutation.GenerateNewCells(jsonData);
            CellField.AddCell(generatedCells);
        }
    }
}