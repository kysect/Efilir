using GenericLife.Models;

namespace GenericLife.Tools
{
    public class AngleRotation
    {
        public static FieldPosition GetRotation(int pos)
        {
            switch (pos)
            {
                case 0:
                    return new FieldPosition(0, -1);
                case 1:
                    return new FieldPosition(1, -1);
                case 2:
                    return new FieldPosition(1, 0);
                case 3:
                    return new FieldPosition(1, 1);
                case 4:
                    return new FieldPosition(0, 1);
                case 5:
                    return new FieldPosition(-1, 1);
                case 6:
                    return new FieldPosition(-1, 0);
                case 7:
                    return new FieldPosition(-1, -1);
                default:
                    return new FieldPosition(0, -1);
            }
        }
    }
}