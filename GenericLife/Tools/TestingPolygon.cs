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
            for (int i = 0; i < 64; i++)
            {
                _cellFieldService.AddRandomCell();
            }

            for (int i = 0; i < 200; i++)
            {
                _cellFieldService.AddFood();
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