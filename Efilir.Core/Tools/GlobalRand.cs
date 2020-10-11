using System;
using System.Collections.Generic;
using Efilir.Core.Types;

namespace Efilir.Core.Tools
{
    public static class GlobalRand
    {
        private static readonly Random Rnd = new Random();

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

        public static Coordinate GeneratePosition(int maxSize)
        {
            return new Coordinate(Next(maxSize), Next(maxSize));
        }
    }
}