using GenericLife.Interfaces;
using GenericLife.Types;

namespace GenericLife.Models.Cells
{
    public class WallCell : IBaseCell
    {
        public Coordinate Position { get; set; }
        public GameArea Field { get; set; }
        public void MakeTurn()
        {
        }
    }
}