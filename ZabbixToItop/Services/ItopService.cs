using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ZabbixToItop.Models;
using ZabbixToItop.Exceptions;
using ZabbixToItop.Settings;
using ZabbixToItop.Util;

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
            this.Client = new HttpClient(httpClientHandler);
            
            this.Settings = new RequestSettings(args);
        }
 
        public ItopService(string[] args, HttpClient client) 
        { 
            this.Client = client;
            this.Settings = new RequestSettings(args);
        }

        public async Task<string> SaveTicketAsync()
        {
            var ticket = new Ticket(this.Settings);
            string ticketJson = Helper.ObjectToJson(ticket);

            var values = new Dictionary<string, string>
            {
                { "auth_pwd", this.Settings.Itop_pwd.Trim() },
                { "auth_user", this.Settings.Itop_user.Trim() },
                { "json_data", ticketJson }
            };

            var requestBody = new FormUrlEncodedContent(values);

            var response = await this.Client.PostAsync(this.Settings.Itop_url, requestBody);

            var itopResponse = Helper.FormatItopResponse(await response.Content.ReadAsStringAsync());

            if (itopResponse.code != 0)
            {
                throw new ItopException(itopResponse.message, itopResponse.code);
            }

            return "code:" + itopResponse.code + " message:" + itopResponse.message;
        }

    }
}
