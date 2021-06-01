using System;
using System.Linq;
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

            //for (int i = 0; i < 400; i++)
            //{
            //    var coordinate = new Vector(GlobalRand.Next(Configuration.FieldSize), GlobalRand.Next(Configuration.FieldSize));
            //    var predefinedCell = new PredefinedCell(
            //        _gameArea,
            //        coordinate,
            //        new Vector(0, 0));
            //    _gameArea.PredefinedCells.Add(predefinedCell);
            //}

            for (double degrees = 0; degrees < 360.0; degrees += 0.5)
            {
                int maxVelocity = 20;
                
                var velocity = new Vector(Math.Sin(Math.PI * degrees / 180.0), Math.Cos(Math.PI * degrees / 180.0)) * maxVelocity;
                var position = new Vector(Configuration.FieldSize / 2.0, Configuration.FieldSize / 2.0);
                var predefinedCell = new PredefinedCell(_gameArea, position, velocity, PredefinedCellType.Red);
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
            _gameArea.PredefinedCells
                .AsParallel()
                .ForAll(c => c.MakeTurn(null));
            _gameArea.UpdatePreviousPositions();
            Application.Current.Dispatcher.Invoke(() => _pd.DrawPoints(_gameArea.PredefinedCells));
        }
    }
}