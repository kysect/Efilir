using System;
using System.Windows;
using Efilir.Client.Tools;
using Efilir.Core.PredefinedCells;
using Efilir.Core.PredefinedCells.Cells;
using Efilir.Core.Tools;
using Efilir.Core.Types;
using Vector = Efilir.Core.Types.Vector;

namespace Efilir.Client.ExecutionContexts
{
    public class PredefinedCellExecutionContext : IExecutionContext
    {
        private bool _isActive;
        private readonly PixelDrawer _pd;
        private readonly PredefinedCellGameArea _gameArea;

        public PredefinedCellExecutionContext(PixelDrawer pd)
        {
            _pd = pd;

            _gameArea = new PredefinedCellGameArea(Configuration.FieldSize);

            //for (int i = 0; i < 1000; i++)
            //{
            //    var predefinedCell = new PredefinedCell(
            //        _gameArea,
            //        new Coordinate(GlobalRand.Next(Configuration.FieldSize), GlobalRand.Next(Configuration.FieldSize)));
            //    _gameArea.PredefinedCells.Add(predefinedCell);
            //}

            for (double degrees = 0; degrees < 360 * 2; degrees += 1.0 / 2)
            {
                int maxVelocity = 40;
                var position = new Vector(Configuration.FieldSize / 2.0, Configuration.FieldSize / 2.0);
                var velocity = new Vector(Math.Sin(Math.PI * degrees / 180.0), Math.Cos(Math.PI * degrees / 180.0)) * maxVelocity;
                var predefinedCell = new PredefinedCell(_gameArea, position, velocity);
                _gameArea.PredefinedCells.Add(predefinedCell);
            }
        }

        public void SetActivity(bool isActive)
        {
            _isActive = isActive;
        }

        public void StartSimulator()
        {
            //TODO: Fix
            _gameArea.PredefinedCells.ForEach(c => c.MakeTurn(null));
            _gameArea.UpdatePreviousPositions();
            Application.Current.Dispatcher.Invoke(() => _pd.DrawPoints(_gameArea.PredefinedCells));
        }
    }
}