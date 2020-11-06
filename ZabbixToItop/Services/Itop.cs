using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ZabbixToItop.Models;

namespace ZabbixToItop.Services
{
    public class Itop
    {
        public Itop() {}

        public string GenerateTicket(ItopConfiguration config)
        {
            TicketFields fields = new TicketFields(config);
           
            Ticket ticket = new Ticket
            {
                Class = config.Class, 
                Status = config.Status,
                Comment = config.Comment, 
                Operation = "core/create",
                Output_fields = "id",
                Fields = fields
            };

            return Utils.ObjectToJson(ticket);
        }

        public async Task<string> SaveTicketOnItopAsync(string jsonString)
        {
            
            string Itop_url = Environment.GetEnvironmentVariable("url"); 
            string Itop_user = Environment.GetEnvironmentVariable("auth_user");
            string Itop_pwd = Environment.GetEnvironmentVariable("auth_pwd"); 

            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
            };

            var client = new HttpClient(httpClientHandler);

            var values = new Dictionary<string, string>
            {
                { "auth_pwd", Itop_pwd },
                { "auth_user", Itop_user },
                { "json_data", jsonString }
            };

            var requestBody = new FormUrlEncodedContent(values);
                    
            var response = await client.PostAsync(Itop_url, requestBody);
            
            var itopResponse = Utils.FormatItopResponse(await response.Content.ReadAsStringAsync());

            if (itopResponse.code != 0)
            {
                throw new ItopException(itopResponse.message, itopResponse.code);
            }
            
            return "code:" + itopResponse.code + " message:" + itopResponse.message;
        }
    }
}
