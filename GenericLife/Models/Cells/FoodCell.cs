using System.Windows.Media;
using GenericLife.Declaration;
using GenericLife.Tools;
using GenericLife.Types;

namespace GenericLife.Models.Cells
{
    public class FoodCell : IBaseCell
    {
        public static int FoodHealthIncome = 10;
        public FieldPosition Position { get; set; }
        public ICellField FieldModel { get; set; }

        public Color GetColor()
        {
            return CellColorGenerator.FoodColor();
        }

        public FoodCell(FieldPosition position)
        {
            Position = position;
        }
    }
}