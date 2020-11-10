using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ZabbixToItop.Models;

namespace ZabbixToItop.Services
{
    public class Itop
    {
        private HttpClient Client { get; set; }

        public Itop() 
        { 
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
            };
            Client = new HttpClient(httpClientHandler);
        }

        public Itop(HttpClient client) 
        { 
            Client = client;
        }

        public async Task<Ticket> GenerateTicketAsync(ItopConfiguration config)
        {
            var fields = new TicketFields(config);
            
            if(config.Service_subcategory_name == null)
            {
                fields.Servicesubcategory_id = await GetServiceSubcategoryByCIAsync(config.Ci);
            }

            var ticket = new Ticket
            {
                Class = config.Class,
                Status = config.Status,
                Comment = config.Comment,
                Operation = "core/create",
                Output_fields = "id",
                Fields = fields
            };

            return ticket;
        }

        public async Task<string> SaveTicketOnItopAsync(string jsonString, string[] args)
        {
            string Itop_url = args[0];
            string Itop_user = args[1];
            string Itop_pwd = args[2];

            var values = new Dictionary<string, string>
            {
                { "auth_pwd", Itop_pwd },
                { "auth_user", Itop_user },
                { "json_data", jsonString }
            };

            var requestBody = new FormUrlEncodedContent(values);

            var response = await Client.PostAsync(Itop_url, requestBody);

            var itopResponse = Utils.FormatItopResponse(await response.Content.ReadAsStringAsync());
           
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
                        { "auth_pwd", "Admin123_" },
                        { "auth_user", "admin7" },
                        { "json_data", "{ \"operation\": \"core/get\", "+
                                        "\"class\": \"Person\", "+
                                        "\"key\": \"SELECT ServiceSubcategory JOIN Service ON ServiceSubcategory.service_id = Service.id JOIN lnkFunctionalCIToService AS lnk ON lnk.service_id = Service.id WHERE functionalci_id_friendlyname = '" + ci + "' AND request_type = 'incident'\", "+
                                        "\"output_fields\": \"friendlyname, email\" }"
                        }
                    };

            var requestBody = new FormUrlEncodedContent(values);
            
            var response = await Client.PostAsync("http://localhost:8000/webservices/rest.php?version=1.3", requestBody);
            
            var itopResponse = await response.Content.ReadAsStringAsync();
            
            var serviceSubcategoryId = Regex.Match(itopResponse, "\"id\":\"(.+?)\"").Groups[1].Value;
            
            return "SELECT ServiceSubcategory JOIN Service ON ServiceSubcategory.service_id = Service.id WHERE ServiceSubcategory.id='" + serviceSubcategoryId + "'";
        }
    }
}
