using System.Windows.Media;

namespace GenericLife.Models
{
    public class FoodCell : IBaseCell
    {
        public static int FoodHealthIncome = 10;
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public Color GetColor()
        {
            var c = new Color();
            c.G = 0;
            c.R = 0;
            c.B = 255;
            return c;
        }

        public FoodCell(int positionX, int positionY)
        {
            PositionY = positionY;
            PositionX = positionX;
        }
    }
}