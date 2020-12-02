using System.Text.RegularExpressions;
using System.Reflection;
using System;
using System.Text.Json;
using ZabbixToItop.Models;
using System.Configuration;

namespace ZabbixToItop.Util
{
    public class Helper 
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
            str = new Regex("[^a-zA-Z0-9 -:_]").Replace(str, "");
            str = new Regex("[ ]{2,}", RegexOptions.None).Replace(str, " ");
            return Regex.Match(str, @str1 + "(.*)" + str2).Groups[1].Value.Trim();
        }

    }
}
