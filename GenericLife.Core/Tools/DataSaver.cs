using System.Collections.Generic;
using System.IO;
using System.Linq;
using GenericLife.Core.CellAbstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GenericLife.Core.Tools
{
    public static class DataSaver
    {
        private const string FileName = @"data.json";

        public static void Save(IEnumerable<IGenericCell> list)
        {
            var dataList = list
                .OrderByDescending(c => c.Age).ThenByDescending(c => c.Health)
                .Take(8)
                .Select(c => new {c.Age, c.Brain.CommandList});

            string dataString = JsonConvert.SerializeObject(dataList);

            using (StreamWriter sw =
                File.Exists(FileName)
                    ? new StreamWriter(FileName)
                    : File.CreateText(FileName))
            {
                sw.WriteLine(dataString);
            }
        }

        public static List<List<int>> Load()
        {
            var gen = new List<List<int>>();

            for (var i = 0; i < 64; i++)
            {
                gen.Add(GlobalRand.GenerateCommandList());
            }

            return gen;
        }
    }
}