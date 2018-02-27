using System.Windows.Media;
using GenericLife.Declaration;
using GenericLife.Tools;

namespace GenericLife.Models.Cells
{
    public class FoodCell : IBaseCell
    {
        public static int FoodHealthIncome = 15;
        public FieldPosition Position { get; set; }
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