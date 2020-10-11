using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using Efilir.Client.ViewModel;
using Efilir.Core.Cells;

namespace Efilir.Client
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel(ImageView);
            _viewModel.Polygon.LoadCells();

            var worker = new BackgroundWorker();
            worker.DoWork += StartSimulation;
            worker.RunWorkerAsync();
        }

        private void Start_ButtonClick(object sender, RoutedEventArgs e)
        {
            _viewModel.IsActive = true;
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.IsActive = false;
        }

        private void StartSimulation(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                while (_viewModel.IsActive)
                {
                    _viewModel.StartSimulator();
                    UpdateInfoBox();
                    Thread.Sleep(300);
                }

                Thread.Sleep(100);
            }
        }

        private void UpdateInfoBox()
        {
            List<IGenericCell> cellList = _viewModel.Polygon.CellField.GetAllGenericCells();

            IOrderedEnumerable<IGenericCell> orderByDescending = cellList
                .OrderByDescending(c => c.Age)
                .ThenByDescending(c => c.Health);

            // UI-thread executing.
            Application.Current.Dispatcher.Invoke(() =>
            {
                CellData.ItemsSource = orderByDescending;
            });
        }
    }
}
