using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinqExample.LinqDemo.DictionaryExample
{
    public static class ListToDictionaryExtensions
    {
        public static Dictionary<string, T> ToDictionaryByFields<T>(this IEnumerable<T> list, params string[] fields)
        {
            var type = typeof(T);
            var properties = fields
                .Select(f => type.GetProperty(f, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase))
                .ToArray();

            if (properties.Any(p => p == null))
                throw new ArgumentException("One or more fields do not exist on the type.");

            //如果重复了，调用下面这一段就会报错
            /*
            return list.ToDictionary(
                item => string.Join("|", properties.Select(p => p.GetValue(item)?.ToString() ?? "")),
                item => item
            );
            */
            var dict = new Dictionary<string, T>();
            foreach (var item in list)
            {
                var key = string.Join("|", properties.Select(p => p.GetValue(item)?.ToString() ?? ""));
                dict.TryAdd(key, item); // 如果 key 已存在，忽略
            }
            return dict;
        }

        public static Dictionary<string, List<T>> ToDictionaryByFieldsAllowDup<T>(this IEnumerable<T> list, params string[] fields)
        {
            var type = typeof(T);
            var properties = fields
                .Select(f => type.GetProperty(f, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase))
                .ToArray();

            if (properties.Any(p => p == null))
                throw new ArgumentException("One or more fields do not exist on the type.");

            return list.GroupBy(item => string.Join("|", properties.Select(p => p.GetValue(item)?.ToString() ?? "")))
                .ToDictionary(g => g.Key, g => g.ToList());
        }
    }
}
