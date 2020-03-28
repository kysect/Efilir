using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using GenericLife.Core.Cells;
using GenericLife.ViewModel;

namespace GenericLife
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel(ImageView);
        }

        private void Start_ButtonClick(object sender, RoutedEventArgs e)
        {
            _viewModel.IsActive = true;
            _viewModel.Polygon.LoadCells();

            var worker = new BackgroundWorker();
            worker.DoWork += StartSimulation;
            worker.RunWorkerAsync();
        }

        private void StartSimulation(object sender, DoWorkEventArgs e)
        {
            while (_viewModel.IsActive)
            {
                _viewModel.Reload();
                _viewModel.StartSimulator();
                UpdateInfoBox();
                Thread.Sleep(300);
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