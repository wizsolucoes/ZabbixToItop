using System.Text.RegularExpressions;
using System.Reflection;
using System;
using System.Text.Json;
using ZabbixToItop.Models;
using System.Configuration;

namespace ZabbixToItop.Services
{
    public class Utils 
    {
        public static string ObjectToJson(Object obj)
        {
            JsonSerializerOptions serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                IgnoreNullValues = true
            };
            return JsonSerializer.Serialize(obj, serializeOptions);
        }

        public static ItopResponse FormatItopResponse(string response)
        {
            var itopResponse = JsonSerializer.Deserialize<ItopResponse>(response);

            itopResponse.message = itopResponse.message == null ? "created" : itopResponse.message;

            return itopResponse;
        }

        public static string GetStringBetween(string str, string str1, string str2)
        {
            str = str.Replace("^M", "");
            str = str.Replace(System.Environment.NewLine, ""); 
            var start = str.IndexOf(str1) +  str1.Length;
            return str.Substring(start, str.IndexOf(str2) - start);
        }
    }
}
