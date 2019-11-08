using System.Collections.Generic;
using System.IO;
using System.Linq;
using GenericLife.Core.CellAbstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GenericLife.Core.Tools
{
    public static class JsonSaver
    {
        private const string FileName = @"data.json";

        public static void Save(IEnumerable<IGenericCell> list)
        {
            var dataList = list
                .OrderByDescending(c => c.Age).ThenByDescending(c => c.Health)
                .Take(8)
                .Select(c => new {c.Age, c.Brain.CommandList});

            var dataString = JsonConvert.SerializeObject(dataList);

            if (!File.Exists(FileName))
                using (var sw = File.CreateText(FileName))
                {
                    sw.WriteLine(dataString);
                }
            else
                using (var sw = new StreamWriter(FileName))
                {
                    sw.WriteLine(dataString);
                }
        }

        public static List<List<int>> Load()
        {
            var gen = new List<List<int>>();

            if (File.Exists(FileName) == false)
            {
                for (int i = 0; i < 8; i++)
                {
                    gen.Add(GlobalRand.GenerateCommandList());
                }
            }
            else
            {
                var dataString = File.ReadAllText(FileName);
                var list = JArray.Parse(dataString);
                foreach (var obj in list) gen.Add(obj["CommandList"].ToObject<List<int>>());
            }

            return gen;
        }
    }
}