using LinqExample.LinqDemo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExample.LinqDemo.SetExample
{
    //最高性能，比linq去重要快
    class List2Set
    {
        //将名字一样的，之选第一个，按照顺序
        public static void Run()
        {
            var people = new List<Person>
            {
                new Person { Name = "Alice", Age = 30 },
                new Person { Name = "Bob", Age = 20 },
                new Person { Name = "alice", Age = 30 }, // 重复（忽略大小写）
                new Person { Name = "Bob", Age = 21 }
            };
            var seen = new HashSet<string>();
            var result = new List<Person>();

            foreach (var person in people)
            {
                if (seen.Add(person.Name))
                {
                    result.Add(person);
                }
            }

            foreach (var person in result)
            {
                Console.WriteLine($"dup set{person.Name} - {person.Age}");
            }
        }

        //将名字一样的，选年龄最大的那个，时间复杂度O(n),借助Dictionary
        public static void RunReduceDup()
        {
            var people = new List<Person>
            {
                new Person { Name = "Alice", Age = 31 },
                new Person { Name = "Bob", Age = 20 },
                new Person { Name = "alice", Age = 30 }, // 重复（忽略大小写）
                new Person { Name = "Bob", Age = 21 }
            };
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

            var result = new HashSet<Person>(dict.Values);

            foreach (var person in result)
            {
                Console.WriteLine($"dup set{person.Name} - {person.Age}");
            }
        }

        //将名字一样的，选年龄最大的那个，时间复杂度O(n logn),先将list排序，然后直接转，性能较低
        public static void RunReduceDup1()
        {
            var people = new List<Person>
            {
                new Person { Name = "Alice", Age = 31 },
                new Person { Name = "Bob", Age = 20 },
                new Person { Name = "alice", Age = 30 }, // 重复（忽略大小写）
                new Person { Name = "Bob", Age = 21 }
            };
            var sorted = people
                .OrderByDescending(p => p.Age)
                .ToList();

            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var result = new HashSet<Person>();

            foreach (var person in sorted)
            {
                if (seen.Add(person.Name))
                {
                    result.Add(person);
                }
            }

            foreach (var person in result)
            {
                Console.WriteLine($"dup set{person.Name} - {person.Age}");
            }
        }
    }
}
