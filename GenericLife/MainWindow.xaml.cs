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

namespace GenericLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DrawingService _ds;
        private CellField _cellField;
        public MainWindow()
        {
            InitializeComponent();
            _ds = new DrawingService(ImageView);
            _cellField = new CellField();
            Random rand = new Random();
            for (int i = 0; i < 100; i++)
            {
                int x = rand.Next(100);
                int y = rand.Next(100);
                if (_cellField.GetPointType(x, y) == PointType.Void)
                {
                    _cellField.Cells.Add(new SimpleCell(x, y, 100));
                }
            }
            _ds.DrawPoints(_cellField.Cells);
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
                    _cellField.RandomMove();
                    _ds.DrawPoints(_cellField.Cells);
                }, null);
                Thread.Sleep(100);
            }
        }
    }
}
