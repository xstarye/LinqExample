using LinqExample.LinqDemo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExample.LinqDemo.DictionaryExample
{
    public static class L2DbyC
    {
        public static void Run()
        {
            var people = new List<PersonE>
            {
                new PersonE { Name = "Alice", Age = 30, City = "Auckland" },
                new PersonE { Name = "Bob", Age = 20, City = "Wellington" },
                new PersonE { Name = "Alice1", Age = 30, City = "Christchurch" } // Duplicate key if fields are only Name + Age
            };

            var dict = people.ToDictionaryByFields("Name", "Age");

            // 输出结果
            foreach (var kv in dict)
            {
                Console.WriteLine($"Key: {kv.Key} -> {kv.Value.City}");
            }
        }

        public static void RunDup()
        {
            var people = new List<PersonE>
            {
                new PersonE { Name = "Alice", Age = 30, City = "Auckland" },
                new PersonE { Name = "Bob", Age = 20, City = "Wellington" },
                new PersonE { Name = "Alice", Age = 30, City = "Christchurch" } // Duplicate key if fields are only Name + Age
            };

            var dict = people.ToDictionaryByFieldsAllowDup("Name", "Age");

            // 输出结果
            foreach (var kv in dict)
            {
                Console.WriteLine($"Key: {kv.Key} -> Count: {kv.Value.Count}");
                foreach (var person in kv.Value)
                {
                    Console.WriteLine($"  - {person.Name}, {person.Age}, {person.City}");
                }
            }
        }

        //如果是要取相同的名字，但是年龄最大的那个
        public static void RunDupMaxAge()
        {
            var people = new List<Person>
            {
                new Person { Name = "Alice", Age = 29 },
                new Person { Name = "Bob", Age = 20 },
                new Person { Name = "alice", Age = 30 }, // 重复（忽略大小写）
                new Person { Name = "Bob", Age = 21 }
            };
            //var dict = new Dictionary<string, Person>();
            //忽略大小写
            var dict = new Dictionary<string, Person>(StringComparer.OrdinalIgnoreCase);

            foreach (var person in people)
            {
                if (dict.TryGetValue(person.Name, out var existing))
                {
                    if (person.Age > existing.Age)
                        dict[person.Name] = person;
                }
                else
                {
                    dict[person.Name] = person;
                }
            }
            foreach (var person in dict)
            {
                Console.WriteLine($"{dict[person.Key].Name} - {dict[person.Key].Age}");
            }
            var result = dict.Values.ToList();
            Console.WriteLine(result.Count());
        }
    }
}
