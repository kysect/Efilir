using System.Net.Mime;
using System.Windows.Controls;
using GenericLife.Declaration;
using GenericLife.Declaration.Cells;

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
    }
}