using System.Windows.Media;
using GenericLife.Declaration;

namespace GenericLife.Models
{
    public class FoodCell : IBaseCell
    {
        public static int FoodHealthIncome = 20;
        public FieldPosition Position { get; set; }
        public Color GetColor()
        {
            var c = new Color();
            c.G = 0;
            c.R = 0;
            c.B = 255;
            return c;
        }

        public FoodCell(FieldPosition position)
        {
            Position = position;
        }
    }
}