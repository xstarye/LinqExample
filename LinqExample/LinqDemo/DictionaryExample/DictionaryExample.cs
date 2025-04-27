using LinqExample.LinqDemo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExample.LinqDemo.DictionaryExample
{
    public class DictionaryExample
    {
        public static void RunKeyObj()
        {
            var people = new List<Person>
            {
                new Person { Name = "Alice", Age = 30 },
                new Person { Name = "Bob", Age = 20 }
            };

            var dict = people.ToDictionary(p => p.Name);//有重复值会报错

            // 访问
            Console.WriteLine(dict["Alice"].Age); // 输出 30
        }

        public static void RunKeyObj_Dup()
        {
            var people = new List<Person>
            {
                new Person { Name = "Alice", Age = 30 },
                new Person { Name = "Alice", Age = 30 },
                new Person { Name = "Bob", Age = 20 }
            };

            var dict = new Dictionary<string, Person>();
            foreach (var p in people)
            {
                dict[p.Name] = p; // 自动覆盖旧值
            }

            // 访问
            Console.WriteLine(dict["Alice"].Age); // 输出 30
        }

        public static void RunKeyInt()
        {
            var people = new List<Person>
            {
                new Person { Name = "Alice", Age = 30 },
                new Person { Name = "Bob", Age = 20 }
            };

            var dict = people.ToDictionary(p => p.Name, p => p.Age);

            // 访问
            Console.WriteLine(dict["Alice"]); // 输出 30
        }

        public static void RunDupKey()
        {
            var people = new List<Person>
            {
                new Person { Name = "Alice", Age = 30 },
                new Person { Name = "Bob", Age = 20 },
                new Person { Name = "Alice", Age = 31 }
            };

            var dict = people.ToDictionary(p => $"{p.Name}|{p.Age}", p => p);

            // 访问
            var key = "Alice|30";
            if (dict.TryGetValue(key, out var person))
            {
                Console.WriteLine($"{person.Name} is {person.Age} years old.");
            }
        }
    }
}
