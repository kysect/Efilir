using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using GenericLife.Models;
using GenericLife.Services;

namespace GenericLife.Tools
{
    public class TestingPolygon
    {
        private readonly DrawingService _ds;
        private CellFieldService _cellFieldService;

        public TestingPolygon(DrawingService ds, CellFieldService cellFieldService)
        {
            _ds = ds;
            _cellFieldService = cellFieldService;

            Init();
        }

        private void Init()
        {
            for (int i = 0; i < 100; i++)
            {
                int x = GlobalRand.Next(100);
                int y = GlobalRand.Next(100);
                if (_cellFieldService.GetPointType(x, y) == PointType.Void)
                {
                    _cellFieldService.Cells.Add(new SimpleCell(_cellFieldService, x, y, 100));
                }
            }

            for (int i = 0; i < 200; i++)
            {
                int x = GlobalRand.Next(100);
                int y = GlobalRand.Next(100);
                if (_cellFieldService.GetPointType(x, y) == PointType.Void)
                {
                    _cellFieldService.Foods.Add(new FoodCell(x, y));
                }
            }

            UpdateUi();
        }

        public void UpdateUi()
        {
            _ds.ClearBlack();
            _ds.DrawPoints(_cellFieldService.Cells);
            _ds.DrawPoints(_cellFieldService.Foods);
        }
        public void RandomMove()
        {
            _cellFieldService.RandomMove();
            UpdateUi();
        }
    }
}