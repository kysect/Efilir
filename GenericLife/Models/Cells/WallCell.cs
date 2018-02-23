using System.Windows.Media;
using GenericLife.Declaration;
using GenericLife.Declaration.Cells;
using GenericLife.Types;

namespace GenericLife.Models.Cells
{
    public class WallCell : IBaseCell
    {
        public FieldPosition Position { get; set; }
        public ICellField FieldModel { get; set; }
    }
}