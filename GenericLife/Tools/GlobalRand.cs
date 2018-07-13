using System;
using System.Collections.Generic;
using GenericLife.Types;

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
            for (var i = 0; i < count; i++) list.Add(Next(maxValue));

            return list;
        }

        public static List<int> GenerateCommandList()
        {
            return RandomList(64, 64);
        }

        public static Coordinate GeneratePosition(int maxSize = Configuration.FieldSize)
        {
            return new Coordinate(Next(maxSize), Next(maxSize));
        }
    }
}