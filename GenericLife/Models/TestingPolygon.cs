using System;
using System.Windows;

namespace GenericLife.Models
{
    public class TestingPolygon
    {
        private readonly ImageDrawingTool _ds;
        public CellFieldModel CellFieldModel;

        public TestingPolygon(ImageDrawingTool ds, CellFieldModel cellFieldModel)
        {
            _ds = ds;
            CellFieldModel = cellFieldModel;

            Init();
        }

        private void Init()
        {
            for (int i = 0; i < 64; i++)
            {
                CellFieldModel.AddGenericCell();
            }
            
            for (int i = 0; i < CellFieldModel.FoodCount; i++)
            {
                CellFieldModel.AddFood();
            }

            UpdateUi();
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
            Application.Current.Dispatcher.Invoke(UpdateUi);
        }
    }
}