using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GenericLife.Models.Cells;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GenericLife.Tools
{
    public class JsonSaver
    {
        private const string FileName = @"data.json";

        public static void Save(IEnumerable<GenericCell> list)
        {
            var dataList = list
                .OrderByDescending(c => c.Age).ThenByDescending(c => c.Health)
                .Take(8)
                .Select(c => new {c.Age, CommandList = c.GetCommandList()});
            var dataString = JsonConvert.SerializeObject(dataList);

            if (!File.Exists(FileName))
            {
                using (StreamWriter sw = File.CreateText(FileName))
                {
                    sw.WriteLine(dataString);
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(FileName))
                {
                    sw.WriteLine(dataString);
                }
            }
        }

        public static List<List<int>> Load()
        {
            string dataString = System.IO.File.ReadAllText(FileName);
            var list = JArray.Parse(dataString);
            List<List<int>> gen = new List<List<int>>();
            foreach (var obj in list)
            {
                gen.Add(obj["CommandList"].ToObject<List<int>>());
            }

            return gen;
        }
    }
}