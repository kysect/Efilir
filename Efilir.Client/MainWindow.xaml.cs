using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using Efilir.Client.ExecutionContexts;
using Efilir.Client.Tools;
using Efilir.Core.Generics.Cells;
using Efilir.Core.Tools;

namespace Efilir.Client
{
    public partial class MainWindow : Window, ICellStatConsumer
    {
        public IExecutionContext GenericExecutionContext { get; }

        public MainWindow()
        {
            InitializeComponent();

            GenericExecutionContext = CreatePredefined();

            var worker = new BackgroundWorker();
            worker.DoWork += StartSimulation;
            worker.RunWorkerAsync();
        }

        private void Start_ButtonClick(object sender, RoutedEventArgs e)
        {
            GenericExecutionContext.SetActivity(true);
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            GenericExecutionContext.SetActivity(false);
        }

        private void StartSimulation(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                GenericExecutionContext.StartSimulator();
                Thread.Sleep(300);
            }
        }

        public void NotifyStatUpdate(IReadOnlyCollection<IGenericCell> cellStatistic)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CellData.ItemsSource = cellStatistic;
            });
        }

        private IExecutionContext CreateGenericContext()
        {
            var pixelDrawer = new PixelDrawer(ImageView, Configuration.FieldSize, Configuration.ScaleSize);
            return new GenericExecutionContext(pixelDrawer, this);
        }

        private IExecutionContext CreatePredefined()
        {
            var pixelDrawer = new PixelDrawer(ImageView, Configuration.FieldSize, Configuration.ScaleSize);
            return new PredefinedCellExecutionContext(pixelDrawer);
        }
    }
}