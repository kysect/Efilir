namespace GenericLife.Models
{
    public class FieldPosition
    {
        public FieldPosition(int x, int y)
        {
            Y = y;
            X = x;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public static FieldPosition operator +(FieldPosition left, FieldPosition right)
        {
            var newX = left.X + right.X;
            var newY = left.Y + right.Y;
            return new FieldPosition(newX, newY);
        }
    }
}