namespace Efilir.Core.Environment
{
    public interface IExecutionContext
    {
        void OnRoundStart();
        bool OnIterationStart();
        void OnRoundEnd();
        void OnUiRender();
    }
}