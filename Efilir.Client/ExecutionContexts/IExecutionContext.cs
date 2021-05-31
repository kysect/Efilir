namespace Efilir.Client.ExecutionContexts
{
    public interface IExecutionContext
    {
        void SetActivity(bool isActive);
        void StartSimulator();
    }
}