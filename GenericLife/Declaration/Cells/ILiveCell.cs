namespace GenericLife.Declaration.Cells
{
    public interface ILiveCell : IBaseCell
    {
        int Health { get; set; }
        int Age { get; set; }
        void TurnAction();
        bool IsAlive();
    }
}