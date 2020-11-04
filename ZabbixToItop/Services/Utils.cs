using System.Reflection;
using System;
using System.Net.Http;
using System.Text.Json;
using ZabbixToItop.Models;
using System.Threading.Tasks;
using ZabbixToItop.Interfaces;
using System.Collections.Specialized;
using ZabbixToItop.Services;
using System.Collections.Generic;
using System.Configuration;

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

        public AppSettingsSection GenerateAppConfig()
        {
            object[] attr = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyConfigurationAttribute), false);
            AssemblyConfigurationAttribute aca = (AssemblyConfigurationAttribute)attr[0];

            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            AppSettingsSection appSettingSection = (AppSettingsSection)config.GetSection(aca.Configuration);
            // Console.WriteLine(appSettingSection.Settings["url"].Value);
            return appSettingSection;
        }
    }
}
