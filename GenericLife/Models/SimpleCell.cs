using System.Windows.Media;

namespace GenericLife.Models
{
    public class SimpleCell
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public int HitPoint { get; set; }

        public Color ColorState
        {
            get
            {
                var c = new Color();
                c.G = (byte) (HitPoint * 2);
                c.R = (byte) (200 - c.G);
                c.B = 0;
                return c;
            }
        }

        public SimpleCell(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;

            HitPoint = 100;
        }

        public SimpleCell(int positionX, int positionY, byte hitpoint)
        {
            PositionX = positionX;
            PositionY = positionY;

            HitPoint = hitpoint;
        }
    }
}