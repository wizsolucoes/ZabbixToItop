using System.Text.RegularExpressions;
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
            ticket.Fields.Servicesubcategory_id = await GetServiceSubcategoryByCIAsync(Settings.Ci);
            string ticketJson = Helper.ObjectToJson(ticket);

            var values = new Dictionary<string, string>
            {
                { "auth_pwd", Settings.Itop_pwd },
                { "auth_user", Settings.Itop_user },
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

        public async Task<string> GetServiceSubcategoryByCIAsync(string ci)
        {
            var values = new Dictionary<string, string>
                    {
                        { "auth_pwd", Settings.Itop_pwd },
                        { "auth_user", Settings.Itop_user },
                        { "json_data", "{ \"operation\": \"core/get\", "+
                                        "\"class\": \"Person\", "+
                                        "\"key\": \"SELECT ServiceSubcategory JOIN Service ON ServiceSubcategory.service_id = Service.id JOIN lnkFunctionalCIToService AS lnk ON lnk.service_id = Service.id WHERE functionalci_id_friendlyname = '" + ci + "' AND request_type = 'incident'\", "+
                                        "\"output_fields\": \"friendlyname, email\" }"
                        }
                    };

            var requestBody = new FormUrlEncodedContent(values);
            
            var response = await Client.PostAsync(Settings.Itop_url, requestBody);

            var itopResponse = await response.Content.ReadAsStringAsync();

            var serviceSubcategoryId = Regex.Match(itopResponse, "\"id\":\"(.+?)\"").Groups[1].Value;

            if(serviceSubcategoryId.Equals(""))
            {
                throw new ItopException("Nenhum service subcategory foi encontrado para o ci " + ci, 100);
            }

            return "SELECT ServiceSubcategory JOIN Service ON ServiceSubcategory.service_id = Service.id WHERE ServiceSubcategory.id='" + serviceSubcategoryId + "'";
        }
    }
}
