using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GenericLife.Models;
using GenericLife.Tools;

namespace GenericLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TestingPolygon _testingPolygon;
        public MainWindow()
        {
            InitializeComponent();

            var ds = new ImageDrawingTool(ImageView);
            var cellField = new CellFieldModel();
            _testingPolygon = new TestingPolygon(ds, cellField);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                Application.Current.Dispatcher.BeginInvoke((Action)delegate
                {
                    _testingPolygon.RandomMove();
                    
                }, null);
                Thread.Sleep(100);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var cellList = _testingPolygon.CellFieldModel.Cells;
            var orderByDescending = cellList.OrderByDescending(c => c.Age);
            CellData.ItemsSource = orderByDescending;
        }
    }
}
