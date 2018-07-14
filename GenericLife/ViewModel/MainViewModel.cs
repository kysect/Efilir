using System.Windows;
using System.Windows.Controls;
using GenericLife.Models;

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
    }
}