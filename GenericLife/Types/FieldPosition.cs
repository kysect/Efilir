namespace GenericLife.Types
{
    public class FieldPosition
    {
        public FieldPosition(int x, int y)
        {
            Y = y;
            X = x;
        }

        public int X { get; }
        public int Y { get; }

        public static FieldPosition operator +(FieldPosition left, FieldPosition right)
        {
            var newX = left.X + right.X;
            var newY = left.Y + right.Y;
            return new FieldPosition(newX, newY);
        }
    }
}