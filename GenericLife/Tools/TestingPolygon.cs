using System.Collections.Generic;
using System.Windows.Controls;
using GenericLife.Core.Algorithms;
using GenericLife.Core.Cells;
using GenericLife.Core.Environment;
using GenericLife.Core.Tools;

namespace GenericLife.Tools
{
    public class TestingPolygon
    {
        private readonly PixelDrawer _pd;
        public readonly SimulationManger CellField;

        public TestingPolygon(PixelDrawer pd, SimulationManger cellField)
        {
            _pd = pd;
            CellField = cellField;
        }

        public TestingPolygon(Image image)
        {
            _pd = new PixelDrawer(image);
            int[,] field = LoadField();
            if(field == null)
            {
                CellField = new SimulationManger();
            }
            else
            {
                CellField = new SimulationManger(field);
            }
        }


        public void UpdateUi()
        {
            IBaseCell[,] cells = CellField.GetAllCells();
            _pd.DrawPoints(cells);
        }

        public void RandomMove()
        {
            CellField.MakeCellsMove();
        }

        public void SaveCells()
        {
            List<IGenericCell> cells = CellField.GetAllGenericCells();
            DataSaver.Save(cells);
        }

        public void LoadCells()
        {
            List<List<int>> jsonData = DataSaver.Load();
            List<IGenericCell> generatedCells = GeneticCellMutation.GenerateNewCells(jsonData);
            var field = LoadField();
            if(field == null)
                CellField.DeleteAllElements();
            else
                CellField.DeleteAllElements(field);

            CellField.InitializeLiveCells(generatedCells);
        }

        public int[,] LoadField()
        {
            return DataSaver.LoadField();
        }
    }
}