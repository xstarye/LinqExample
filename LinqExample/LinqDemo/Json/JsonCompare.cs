using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.Json;
using Newtonsoft.Json;
using LinqExample.LinqDemo.DTO;

namespace LinqExample.LinqDemo.Json
{
    public class JsonCompare
    {
        public static void run()
        {
            var personList = Enumerable.Range(0, 100000).Select(i => new Person
            {
                Id = i,
                Name = $"Person_{i}",
                Age = i % 100
            }).ToList();

            Console.WriteLine("Benchmarking JSON Serialization/Deserialization\n");

            // Newtonsoft.Json
            var sw1 = Stopwatch.StartNew();
            var json1 = JsonConvert.SerializeObject(personList);
            sw1.Stop();
            Console.WriteLine($"Newtonsoft.Serialize: {sw1.ElapsedMilliseconds} ms");

            sw1.Restart();
            var list1 = JsonConvert.DeserializeObject<List<Person>>(json1);
            sw1.Stop();
            Console.WriteLine($"Newtonsoft.Deserialize: {sw1.ElapsedMilliseconds} ms\n");

            // System.Text.Json
            var sw2 = Stopwatch.StartNew();
            var json2 = JsonHelper.Serialize(personList);
            sw2.Stop();
            Console.WriteLine($"System.Text.Json.Serialize: {sw2.ElapsedMilliseconds} ms");

            sw2.Restart();
            var list2 = JsonHelper.Deserialize<List<Person>>(json2);
            sw2.Stop();
            Console.WriteLine($"System.Text.Json.Deserialize: {sw2.ElapsedMilliseconds} ms");
        }
    }
}
