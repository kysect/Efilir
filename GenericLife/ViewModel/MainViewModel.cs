using System.Windows;
using System.Windows.Controls;
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
            while (!Polygon.SimulationFinished)
            {
                if (!IsActive)
                    return;

                Polygon.SimulateRound();
                //TODO: Fix
                Application.Current.Dispatcher.Invoke(() => Polygon.UpdateUi());
            }
        }

        public void Reload()
        {
            Polygon.SaveCells();
            Polygon.LoadCells();
        }
    }
}