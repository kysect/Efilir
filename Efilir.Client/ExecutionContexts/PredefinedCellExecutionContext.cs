using System.Collections.Generic;
using System.Windows;
using Efilir.Client.Tools;
using Efilir.Core.Environment;
using Efilir.Core.PredefinedCells.Cells;
using Efilir.Core.Tools;
using Efilir.Core.Types;

namespace Efilir.Client.ExecutionContexts
{
    public class PredefinedCellExecutionContext : IExecutionContext
    {
        private bool _isActive;
        private readonly PixelDrawer _pd;
        private readonly GameArea _gameArea;

        private List<PredefinedCell> _cells;

        public PredefinedCellExecutionContext(PixelDrawer pd)
        {
            _pd = pd;

            _gameArea = new GameArea(Configuration.FieldSize);
            _cells = new List<PredefinedCell>();

            for (int i = 0; i < 10; i++)
            {
                _cells.Add(new PredefinedCell(_gameArea, new Coordinate(i + 1, i + 1)));
            }
        }

        public void SetActivity(bool isActive)
        {
            _isActive = isActive;
        }

        public void StartSimulator()
        {
            //TODO: Fix
            _cells.ForEach(c => c.MakeTurn(null));
            Application.Current.Dispatcher.Invoke(() => _pd.DrawPoints(_gameArea.Cells));
        }
    }
}