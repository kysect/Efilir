using System.Threading;
using System.Windows;
using Efilir.Core.Environment;

namespace Efilir.Client.ExecutionContexts
{
    public class BaseExecutionContext
    {
        private bool _isActive;
        private readonly IExecutionContext _executionContext;

        public BaseExecutionContext(IExecutionContext executionContext)
        {
            _executionContext = executionContext;
            _isActive = false;
        }

        public void SetActivity(bool isActive)
        {
            _isActive = isActive;
        }

        public void StartSimulator()
        {
            if (!_isActive)
                return;

            _executionContext.OnRoundStart();
            bool runNextRound;
            do
            {
                if (!_isActive)
                    return;

                runNextRound = _executionContext.OnIterationStart();
                Application.Current.Dispatcher.Invoke(() => _executionContext.OnUiRender());
                Thread.Sleep(50);
            } while (runNextRound);

            _executionContext.OnRoundEnd();
        }
    }
}