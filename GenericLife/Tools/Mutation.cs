using System.Collections.Generic;

namespace GenericLife.Tools
{
    public class Mutation
    {
        public static List<int> GenerateMutant(List<int> commandList)
        {
            List<int> list = commandList;

            int index = GlobalRand.Next(list.Count);
            //TODO: 
            list[index] = GlobalRand.Next(64);

            return list;
        }
    }
}