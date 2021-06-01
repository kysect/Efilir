using System.Windows;
using Efilir.Client.Tools;
using Efilir.Core.PredefinedCells;
using Efilir.Core.PredefinedCells.Cells;
using Efilir.Core.Tools;
using Efilir.Core.Types;

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

            for (int i = 0; i < 1000; i++)
            {
                var predefinedCell = new PredefinedCell(_gameArea, new Coordinate(GlobalRand.Next(Configuration.FieldSize), GlobalRand.Next(Configuration.FieldSize)));
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
            Application.Current.Dispatcher.Invoke(() => _pd.DrawPoints(_gameArea.PredefinedCells));
        }
    }
}