using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GenericLife.Declaration;
using GenericLife.Models;
using GenericLife.Models.Cells;
using GenericLife.Tools;

namespace GenericLife.ViewModel
{
    public class MainViewModel
    {
        public MainViewModel(Image image)
        {
            Polygon = new TestingPolygon(image);
            IsActive = false;
        }

        public TestingPolygon Polygon { get; }
        public bool IsActive { get; set; }

        public void StartSimulator()
        {
            while (!Polygon.CellField.AliveLessThanEight())
            {
                if (!IsActive) return;
                Polygon.RandomMove();
                Application.Current.Dispatcher.Invoke(() => Polygon.UpdateUi());
            }
        }

        public void HiddenSimulation()
        {
            while (!Polygon.CellField.AliveLessThanEight())
            {
                if (IsActive)
                    Polygon.RandomMove();
            }
        }
    }
}