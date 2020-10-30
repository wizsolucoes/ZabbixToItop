using System;
using System.Net.Http;
using System.Text.Json;
using ZabbixToItop.Models;
using System.Threading.Tasks;
using ZabbixToItop.Interfaces;

namespace ZabbixToItop.Services
{
    public class Utils : IUtils
    {
        public string ObjectToJson(Object obj)
        {
            JsonSerializerOptions serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                IgnoreNullValues = true
            };
            return JsonSerializer.Serialize(obj, serializeOptions);
        }

        public ItopResponse FormatItopResponse(string response)
        {
            var itopResponse = JsonSerializer.Deserialize<ItopResponse>(response);

            itopResponse.message = itopResponse.message == null ? "created" : itopResponse.message;

            return itopResponse;
        }
    }
}
