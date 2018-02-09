namespace GenericLife.Tools
{
    public class AngleRotation
    {
        public static (int x, int y) GetRotation(int pos)
        {
            switch (pos)
            {
                case 0:
                    return (0, -1);
                case 1:
                    return (1, -1);
                case 2:
                    return (1, 0);
                case 3:
                    return (1, 1);
                case 4:
                    return (0, 1);
                case 5:
                    return (-1, 1);
                case 6:
                    return (-1, 0);
                case 7:
                    return (-1, -1);
                default:
                    return (0, -1);
            }
        }
    }
}