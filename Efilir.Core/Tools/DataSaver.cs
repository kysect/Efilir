using System.Collections.Generic;
using System.IO;
using System.Linq;
using Efilir.Core.Cells;
using Newtonsoft.Json;

namespace Efilir.Core.Tools
{
    public static class DataSaver
    {
        private const string FileName = @"data.json";
        private const string FieldFileName = @"dataField.json";

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

            for (var i = 0; i < Configuration.LiveCellCount; i++)
            {
                gen.Add(GlobalRand.RandomList(Configuration.GenCount, Configuration.GenMaxValue));
            }

            return gen;
        }

        public static int[,] LoadField()
        {
            if (File.Exists(FieldFileName) == false)
                return null;

            return JsonConvert.DeserializeObject<int[,]>(File.ReadAllText(FieldFileName));
        }
    }
}