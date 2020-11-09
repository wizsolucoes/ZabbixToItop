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

        public Ticket GenerateTicket(ItopConfiguration config)
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

            return ticket;
        }

        public async Task<string> SaveTicketOnItopAsync(string jsonString, string[] args)
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
            };

            var client = new HttpClient(httpClientHandler);

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
