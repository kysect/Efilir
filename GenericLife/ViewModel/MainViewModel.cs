using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using GenericLife.Declaration;
using GenericLife.Models;
using GenericLife.Models.Cells;
using GenericLife.Tools;

namespace GenericLife.ViewModel
{
    public class MainViewModel
    {
        public MainViewModel(Image image)
        {
            var drawingTool = new ImageDrawingTool(image);
            var cellField = new CellFieldModel();
            Polygon = new TestingPolygon(drawingTool, cellField);
            IsActive = false;
        }

        public TestingPolygon Polygon { get; }
        public bool IsActive { get; set; }

        public void StartSimulator()
        {
            while (!Polygon.CellFieldModel.IsAliveFew)
            {
                Application.Current.Dispatcher.BeginInvoke((Action) delegate
                {
                    if (IsActive)
                        Polygon.RandomMove();
                }, null);

                Thread.Sleep(100);
            }
        }

        public void SaveCells()
        {
            var cells = Polygon.CellFieldModel.Cells.Select(c => c as GenericCell);
            JsonSaver.Save(cells);
        }

        public void LoadCells()
        {
            var cells = JsonSaver.Load();
            var newList = new List<ILiveCell>();
            foreach (var cell in cells)
            {
                for (var i = 0; i < 2; i++)
                {
                    var cellBrain = new CellBrain
                    {
                        Field = Polygon.CellFieldModel,
                        CommandList = Mutation.GenerateMutant(cell)
                    };

                    var newCell = new GenericCell(Polygon.CellFieldModel,
                        GlobalRand.GeneratePosition())
                    {
                        Brain = cellBrain
                    };

                    cellBrain.Cell = newCell;
                    Polygon.CellFieldModel.Cells.Add(newCell);
                    newList.Add(newCell);
                }

                for (var i = 0; i < 6; i++)
                {
                    var cellBrain = new CellBrain
                    {
                        Field = Polygon.CellFieldModel,
                        CommandList = new List<int>(cell)
                    };

                    var newCell = new GenericCell(Polygon.CellFieldModel,
                        GlobalRand.GeneratePosition())
                    {
                        Brain = cellBrain
                    };

                    cellBrain.Cell = newCell;
                    Polygon.CellFieldModel.Cells.Add(newCell);
                    newList.Add(newCell);
                }
            }

            Polygon.CellFieldModel.Cells = newList;
        }
    }
}