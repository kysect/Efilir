using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using GenericLife.Declaration;
using GenericLife.Models;
using GenericLife.Models.Cells;
using GenericLife.Tools;

namespace GenericLife
{
    public partial class MainWindow : Window
    {
        private readonly TestingPolygon _testingPolygon;
        private bool _isActive;

        public MainWindow()
        {
            InitializeComponent();

            var ds = new ImageDrawingTool(ImageView);
            var cellField = new CellFieldModel();
            _testingPolygon = new TestingPolygon(ds, cellField);
            _isActive = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _isActive = true;
            var worker = new BackgroundWorker();
            //worker.DoWork += Worker_DoWork;
            worker.DoWork += Endless;
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (var i = 0; i < 100; i++)
            {
                if (_isActive == false && _testingPolygon.CellFieldModel.IsAnyAlive)
                {
                    return;
                }
                Application.Current.Dispatcher.BeginInvoke((Action) delegate { _testingPolygon.RandomMove(); }, null);
                Thread.Sleep(100);
            }
        }

        private void Endless(object sender, DoWorkEventArgs e)
        {
            while (_isActive)
            {
                while (!_testingPolygon.CellFieldModel.IsAliveFew)
                {
                    Application.Current.Dispatcher.BeginInvoke((Action) delegate
                    {
                        if (_isActive)
                        _testingPolygon.RandomMove();
                    }, null);
                    Thread.Sleep(100);
                }
                Application.Current.Dispatcher.BeginInvoke((Action)delegate { UpdateInfoBox(null, null); }, null);
                Thread.Sleep(200);
                SaveCells(null, null);
                LoadCells(null, null);
                
            }
        }

        private void UpdateInfoBox(object sender, RoutedEventArgs e)
        {
            var cellList = _testingPolygon.CellFieldModel.Cells;
            var orderByDescending = cellList.OrderByDescending(c => c.Age);
            CellData.ItemsSource = orderByDescending;
        }

        private void PassOneTurn(object sender, RoutedEventArgs e)
        {
            _testingPolygon.RandomMove();
        }

        private void PussTenTurn(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < 10; i++) _testingPolygon.RandomMove();
        }

        private void SaveCells(object sender, RoutedEventArgs e)
        {
            var cells = _testingPolygon.CellFieldModel.Cells.Select(c => c as GenericCell);
            JsonSaver.Save(cells);
        }

        private void LoadCells(object sender, RoutedEventArgs e)
        {
            var cells = JsonSaver.Load();
            var newList = new List<ILiveCell>();
            foreach (var cell in cells)
            {
                for (int i = 0; i < 4; i++)
                {
                    var cellBrain = new CellBrain(_testingPolygon.CellFieldModel);
                    cellBrain.CommandList = Mutation.GenerateMutant(cell);

                    var newCell = new GenericCell(_testingPolygon.CellFieldModel,
                        GlobalRand.GeneratePosition(),
                        cellBrain);

                    cellBrain.SetCell(newCell);
                    _testingPolygon.CellFieldModel.Cells.Add(newCell);
                    newList.Add(newCell);
                }

                for (int i = 0; i < 4; i++)
                {
                    var cellBrain = new CellBrain(_testingPolygon.CellFieldModel);
                    cellBrain.CommandList = new List<int>(cell);

                    var newCell = new GenericCell(_testingPolygon.CellFieldModel,
                        GlobalRand.GeneratePosition(),
                        cellBrain);

                    cellBrain.SetCell(newCell);
                    _testingPolygon.CellFieldModel.Cells.Add(newCell);
                    newList.Add(newCell);
                }
            }

            _testingPolygon.CellFieldModel.Cells = newList;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _isActive = false;
            UpdateInfoBox(null, null);
        }
    }
}