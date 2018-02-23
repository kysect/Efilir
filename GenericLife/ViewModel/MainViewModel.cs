using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GenericLife.Declaration;
using GenericLife.Declaration.Cells;
using GenericLife.Models;
using GenericLife.Models.Cells;
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
            while (!Polygon.CellField.AliveLessThanEight())
            {
                Application.Current.Dispatcher.BeginInvoke((Action) delegate
                {
                    if (!IsActive) return;
                    Polygon.RandomMove();
                    Polygon.UpdateUi();
                }, null);

                Thread.Sleep(100);
            }
        }

        public void HiddenSimulation()
        {
            while (!Polygon.CellField.AliveLessThanEight())
            {
                    if (IsActive)
                        Polygon.RandomMove();
            }
        }

        public void SaveCells()
        {
            var cells = Polygon.CellField.Cells.Select(c => c as GenericCell);
            JsonSaver.Save(cells);
        }

        public void LoadCells()
        {
            Polygon.CellField.Cells = new List<ILiveCell>();
            var jsonData = JsonSaver.Load();
            var generatedCells = GeneticCellMutation.GenerateNewCells(jsonData);
            Polygon.CellField.AddCell(generatedCells);
        }
    }
}