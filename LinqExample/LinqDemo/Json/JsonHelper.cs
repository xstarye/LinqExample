using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinqExample.LinqDemo.Json
{
    public static class JsonHelper
    {
        private static readonly JsonSerializerOptions _options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        public static string Serialize<T>(T obj) =>
            JsonSerializer.Serialize(obj, _options);

        public static T Deserialize<T>(string json) =>
            JsonSerializer.Deserialize<T>(json, _options);
    }
}
