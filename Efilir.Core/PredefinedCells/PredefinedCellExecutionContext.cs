using System;
using Efilir.Core.Environment;
using Efilir.Core.PredefinedCells.Cells;
using Efilir.Core.Tools;
using Efilir.Core.Types;

namespace Efilir.Core.PredefinedCells
{
    public class PredefinedCellExecutionContext : IExecutionContext
    {
        private readonly IPixelDrawer _pd;
        private readonly PredefinedCellGameArea _gameArea;

        public PredefinedCellExecutionContext(IPixelDrawer pd)
        {
            _pd = pd;

            _gameArea = new PredefinedCellGameArea(Configuration.FieldSize);

            //for (int i = 0; i < 400; i++)
            //{
            //    var coordinate = new Vector(GlobalRand.Next(Configuration.FieldSize), GlobalRand.Next(Configuration.FieldSize));
            //    var predefinedCell = new PredefinedCell(
            //        _gameArea,
            //        coordinate,
            //        new Vector(0, 0));
            //    _gameArea.PredefinedCells.Add(predefinedCell);
            //}

            for (double degrees = 0; degrees < 360.0; degrees += 1)
            {
                double cellDegree = Math.PI * degrees / 180.0;
                Vector velocity = new Vector(Math.Sin(cellDegree), Math.Cos(cellDegree)) * Configuration.PredefinedCellVelocity;
                var position = new Vector(Configuration.FieldSize / 2.0, Configuration.FieldSize / 2.0);

                var predefinedCell = new VectorBasedPredefinedCell(_gameArea, position, velocity, PredefinedCellType.Red);
                _gameArea.PredefinedCells.Add(predefinedCell);
            }
        }

        public void OnRoundStart()
        {
        }

        public bool OnIterationStart()
        {
            _gameArea.PredefinedCells
                .ForEach(c => c.MakeTurn(null));
            _gameArea.UpdatePreviousPositions();
            return true;
        }

        public void OnRoundEnd()
        {
        }

        public void OnUiRender()
        {
            _pd.DrawPoints(_gameArea.PredefinedCells);
        }
    }
}