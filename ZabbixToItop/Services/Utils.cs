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

        public  static AppSettingsSection GenerateAppConfig()
        {
            //Pegando o tipo de build (Release, Debug)
            object[] attr = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyConfigurationAttribute), false);
            AssemblyConfigurationAttribute aca = (AssemblyConfigurationAttribute)attr[0];

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            AppSettingsSection appSettingSection = (AppSettingsSection)config.GetSection(aca.Configuration);
            return appSettingSection;
        }
    }
}
