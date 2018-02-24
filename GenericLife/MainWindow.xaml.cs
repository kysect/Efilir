using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
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
            _viewModel.Polygon.LoadCells();
        }

        private void Start_ButtonClick(object sender, RoutedEventArgs e)
        {
            _viewModel.IsActive = true;
            var worker = new BackgroundWorker();
            worker.DoWork += StartSimulation;
            worker.RunWorkerAsync();
        }

        private void StartSimulation(object sender, DoWorkEventArgs e)
        {
            while (_viewModel.IsActive)
            {
                _viewModel.StartSimulator();
                UpdateInfoBox();

                _viewModel.Polygon.SaveCells();
                _viewModel.Polygon.LoadCells();
                Thread.Sleep(300);
            }
        }

        private void UpdateInfoBox()
        {
            var cellList = _viewModel.Polygon.CellField.GetAllLiveCells();
            var orderByDescending = cellList
                .OrderByDescending(c => c.Age)
                .ThenByDescending(c => c.Health);

            //Update UI ListBox
            Application.Current.Dispatcher.Invoke(() =>
            {
                CellData.ItemsSource = orderByDescending;
            });
        }
    }
}