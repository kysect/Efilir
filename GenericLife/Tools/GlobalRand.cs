using System;
using System.Collections.Generic;
using GenericLife.Models;

namespace GenericLife.Tools
{
    public static class GlobalRand
    {
        private static readonly Random Rnd;

        static GlobalRand()
        {
            Rnd = new Random();
        }

        public static int Next(int count)
        {
            return Rnd.Next(count);
        }

        public static List<int> RandomList(int count, int maxValue)
        {
            var list = new List<int>();
            for (int i = 0; i < count; i++)
            {
                list.Add(Next(maxValue));
            }

            return list;
        }

        public static List<int> GenerateCommandList()
        {
            var list = new List<int>(64);
            for (int i = 0; i < 64; i++)
            {
                list.Add(Next(64));
            }

            return list;
        }

        public static FieldPosition GeneratePosition()
        {
            int x = Next(100);
            int y = Next(100);
            return new FieldPosition(x, y);
        }
    }
}