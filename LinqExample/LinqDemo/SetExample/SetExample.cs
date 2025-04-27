using LinqExample.LinqDemo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExample.LinqDemo.SetExample
{
    class SetExample
    {
        //这种方式更加智能，效率更高
        public static void Run()
        {
            var people = new List<Person>
            {
                new Person { Name = "Alice", Age = 29 },
                new Person { Name = "Bob", Age = 20 },
                new Person { Name = "alice", Age = 30 }, // 重复（忽略大小写）
                new Person { Name = "Bob", Age = 21 }
            };
            var distinctPeople = people.ToHashSet(new PersonCompare());

            foreach (var person in distinctPeople)
            {
                Console.WriteLine($"{person.Name} - {person.Age}");
            }
            List<Person> newlist = distinctPeople.ToList();
            Console.WriteLine(newlist.Count());
        }

        

        public static void RunSingle()
        {
            var list = new List<string> { "apple", "banana", "apple", "orange" };

            // 方法 1: 使用 ToHashSet()
            var set1 = list.ToHashSet();  // .NET Core 3.0+ / .NET 5+
            foreach (var item in set1)
            {
                Console.WriteLine(item);
            }
        }
    }
}
