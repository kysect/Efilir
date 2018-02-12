using GenericLife.Models.Cells;

namespace GenericLife.Declaration
{
    //TODO: Generic cell
    public interface ICellBrain
    {
        //TODO: Walls
        GenericCell Cell { get; set; }
        ICellField Field { get; set; }
        void MakeTurn();
    }
}