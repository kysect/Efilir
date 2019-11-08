using GenericLife.Core.CellAbstractions;
using GenericLife.Core.Types;

namespace GenericLife.Core.Models.Cells
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