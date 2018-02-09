using System;
using System.Windows.Media;

namespace GenericLife.Tools
{
    public static class CellColorGenerator
    {
        public static Color DeadCell()
        {
            return new Color
            {
                R = byte.MaxValue,
                G = byte.MaxValue,
                B = byte.MaxValue
            };
        }

        public static Color HealthIndicator(int health)
        {
            health = Math.Min(health, 100);
            return new Color
            {
                R = (byte)(200 - health * 2),
                G = (byte)(health * 2),
                B = 0
            };
        }

        public static Color FoodColor()
        {
            return new Color
            {
                R = 0,
                G = 0,
                B = 200
            };
        }

    }
}