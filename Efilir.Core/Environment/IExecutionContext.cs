namespace Efilir.Client.ExecutionContexts
{
    public interface IExecutionContext
    {
        void OnRoundStart();
        bool OnIterationStart();
        void OnRoundEnd();
        void OnUiRender();
    }
}