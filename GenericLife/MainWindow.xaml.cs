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
            _viewModel.LoadCells();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.IsActive = true;
            var worker = new BackgroundWorker();
            worker.DoWork += Endless;
            worker.RunWorkerAsync();
        }

        private void Endless(object sender, DoWorkEventArgs e)
        {
            while (_viewModel.IsActive)
            {
                _viewModel.StartSimulator();

                Application.Current.Dispatcher.BeginInvoke((Action)delegate
                {
                    UpdateInfoBox(null, null);
                }, null);
                Thread.Sleep(200);

                _viewModel.SaveCells();
                _viewModel.LoadCells();
                Thread.Sleep(200);
            }
        }

        private void HideStart(object sender, RoutedEventArgs e)
        {
            _viewModel.IsActive = true;
            while (_viewModel.IsActive)
            {
                _viewModel.HideSimulation();

                _viewModel.SaveCells();
                _viewModel.LoadCells();
            }
        }

        private void UpdateInfoBox(object sender, RoutedEventArgs e)
        {
            var cellList = _viewModel.Polygon.CellFieldModel.Cells;
            var orderByDescending = cellList.OrderByDescending(c => c.Age);
            CellData.ItemsSource = orderByDescending;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _viewModel.IsActive = false;
            UpdateInfoBox(null, null);
        }
    }
}