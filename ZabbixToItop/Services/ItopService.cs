using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ZabbixToItop.Models;
using ZabbixToItop.Exceptions;
using ZabbixToItop.Settings;
using ZabbixToItop.Util;
using System;
namespace ZabbixToItop.Services
{
    public class ItopService
    {
        private HttpClient Client { get; set; }
        private RequestSettings Settings { get; set; }

        public ItopService(string[] args)  
        { 
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
            };
            Client = new HttpClient(httpClientHandler);
            
            Settings = new RequestSettings(args);
        }
 
        public ItopService(string[] args, HttpClient client) 
        { 
            Client = client;
            Settings = new RequestSettings(args);
        }

        public async Task<string> SaveTicketAsync()
        {
            var ticket = new Ticket(Settings);
            string ticketJson = Helper.ObjectToJson(ticket);

            var values = new Dictionary<string, string>
            {
                { "auth_pwd", Settings.Itop_pwd.Trim() },
                { "auth_user", Settings.Itop_user.Trim() },
                { "json_data", ticketJson }
            };

            var requestBody = new FormUrlEncodedContent(values);

            var response = await Client.PostAsync(Settings.Itop_url, requestBody);

            var itopResponse = Helper.FormatItopResponse(await response.Content.ReadAsStringAsync());

            if (itopResponse.code != 0)
            {
                throw new ItopException(itopResponse.message, itopResponse.code);
            }

            return "code:" + itopResponse.code + " message:" + itopResponse.message;
        }

    }
}
