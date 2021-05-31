using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using Efilir.Client.ExecutionContexts;
using Efilir.Core.Generics.Cells;

namespace Efilir.Client
{
    public partial class MainWindow : Window
    {
        public GenericExecutionContext GenericExecutionContext { get; }

        public MainWindow()
        {
            InitializeComponent();
            GenericExecutionContext = new GenericExecutionContext(ImageView);
            GenericExecutionContext.LoadCells();

            var worker = new BackgroundWorker();
            worker.DoWork += StartSimulation;
            worker.RunWorkerAsync();
        }

        private void Start_ButtonClick(object sender, RoutedEventArgs e)
        {
            GenericExecutionContext.IsActive = true;
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            GenericExecutionContext.IsActive = false;
        }

        private void StartSimulation(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                while (GenericExecutionContext.IsActive)
                {
                    GenericExecutionContext.StartSimulator();
                    UpdateInfoBox();
                    Thread.Sleep(300);
                }

                Thread.Sleep(100);
            }
        }

        private void UpdateInfoBox()
        {
            IReadOnlyCollection<IGenericCell> cellList = GenericExecutionContext.GetCellRating();

            // UI-thread executing.
            Application.Current.Dispatcher.Invoke(() =>
            {
                CellData.ItemsSource = cellList;
            });
        }
    }
}
