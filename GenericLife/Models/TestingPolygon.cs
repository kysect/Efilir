namespace GenericLife.Models
{
    public class TestingPolygon
    {
        private readonly ImageDrawingTool _ds;
        public readonly CellFieldModel CellFieldModel;

        public TestingPolygon(ImageDrawingTool ds, CellFieldModel cellFieldModel)
        {
            _ds = ds;
            CellFieldModel = cellFieldModel;
        }


        public void UpdateUi()
        {
            _ds.ClearBlack();
            _ds.DrawPoints(CellFieldModel.Cells);
            _ds.DrawPoints(CellFieldModel.Foods);
        }
        public void RandomMove()
        {
            CellFieldModel.RandomMove();
        }
    }
}