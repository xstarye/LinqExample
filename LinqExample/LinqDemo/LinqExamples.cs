using LinqExample.LinqDemo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExample.LinqDemo
{
    public class LinqExamples
    {
        public static void Run()
        {
            var people = new List<Person>
            {
                new Person { Id = 1, Name = "Alice", Age = 30 },
                new Person { Id = 2, Name = "Bob", Age = 17 },
                new Person { Id = 3, Name = "Charlie", Age = 22 },
                new Person { Id = 4, Name = "Alice", Age = 35 }
            };

            //排序，sort性能高，people = people.OrderBy(p => p.Age).ToList();性能低
            people.Sort((a, b) => a.Age.CompareTo(b.Age));
            foreach (var person in people)
            {
                Console.WriteLine($"sort {person.Name} - {person.Age}");
            }

            // 1. Where (过滤)
            var adults = people.Where(p => p.Age >= 18).ToList();

            // 2. Select (投影)
            var names = people.Select(p => p.Name).ToList();

            // 3. OrderBy / ThenBy
            var sorted = people.OrderBy(p => p.Age).ThenBy(p => p.Name).ToList();

            // 4. GroupBy
            var grouped = people.GroupBy(p => p.Name);
            foreach (var group in grouped)
            {
                Console.WriteLine($"Group: {group.Key}");
                foreach (var person in group)
                    Console.WriteLine($"  {person.Name} - {person.Age}");
            }

            // 4. GroupBy, 并且去重，只保留第一个
            var grouped1 = people.GroupBy(p => p.Name).Select(g => g.First()).ToList();
            foreach (var person in grouped1)
            {
                Console.WriteLine($"dup  {person.Name} - {person.Age}");
            }

            // 5. Any / All
            bool hasTeen = people.Any(p => p.Age < 20);
            bool allAdults = people.All(p => p.Age >= 18);

            // 6. First / FirstOrDefault
            var firstAlice = people.First(p => p.Name == "Alice");
            var unknown = people.FirstOrDefault(p => p.Name == "Unknown");

            // 7. Distinct
            var distinctNames = people.Select(p => p.Name).Distinct().ToList();

            // 8. ToDictionary
            var dict = people.ToDictionary(p => p.Id, p => p.Name);

            // 9. Skip / Take (分页)
            var page = people.Skip(0).Take(2).ToList();

            // 10. Join 示例
            var addresses = new List<(int PersonId, string City)>
            {
                (1, "Beijing"), (2, "Shanghai")
            };
            var joined = people.Join(addresses,
                p => p.Id,
                a => a.PersonId,
                (p, a) => new { p.Name, a.City }).ToList();

            // 输出部分结果
            Console.WriteLine("\n== Adults ==");
            adults.ForEach(p => Console.WriteLine($"{p.Name} ({p.Age})"));

            Console.WriteLine("\n== Names ==");
            names.ForEach(n => Console.WriteLine(n));

            Console.WriteLine("\n== Joined Data ==");
            joined.ForEach(j => Console.WriteLine($"{j.Name} lives in {j.City}"));

            
        }
    }
}
