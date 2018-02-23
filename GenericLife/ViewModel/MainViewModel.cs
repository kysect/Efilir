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
                    if (IsActive)
                        Polygon.RandomMove();
                    Polygon.UpdateUi();
                }, null);

                Thread.Sleep(100);
            }
        }

        public void StartWithDelay()
        {
            while (!Polygon.CellField.AliveLessThanEight())
            {
                if (IsActive)
                    Polygon.RandomMove();
                Polygon.UpdateUi();

                Task.Delay(200);
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
            var cells = JsonSaver.Load();
            foreach (var cell in cells)
            {
                var brain = new CellBrain(cell);

                for (var i = 0; i < 2; i++)
                {
                    var newCell = new GenericCell()
                    {
                        Brain = brain.GenerateChildWithMutant() as CellBrain
                    };

                    Polygon.CellField.AddCell(newCell);
                }

                for (var i = 0; i < 6; i++)
                {
                    var newCell = new GenericCell()
                    {
                        Brain = brain.GenerateChild() as CellBrain
                    };

                    Polygon.CellField.AddCell(newCell);
                }
            }
        }
    }
}