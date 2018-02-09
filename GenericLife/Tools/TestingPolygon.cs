using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using GenericLife.Models;

namespace GenericLife.Tools
{
    public class TestingPolygon
    {
        private readonly ImageDrawingTool _ds;
        private CellFieldModel _cellFieldModel;

        public TestingPolygon(ImageDrawingTool ds, CellFieldModel cellFieldModel)
        {
            _ds = ds;
            _cellFieldModel = cellFieldModel;

            Init();
        }

        private void Init()
        {
            for (int i = 0; i < 64; i++)
            {
                _cellFieldModel.AddRandomCell();
            }

            for (int i = 0; i < 200; i++)
            {
                _cellFieldModel.AddFood();
            }

            UpdateUi();
        }

        public void UpdateUi()
        {
            _ds.ClearBlack();
            _ds.DrawPoints(_cellFieldModel.Cells);
            _ds.DrawPoints(_cellFieldModel.Foods);
        }
        public void RandomMove()
        {
            _cellFieldModel.RandomMove();
            UpdateUi();
        }
    }
}