using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using ZabbixToItop.Interfaces;
using ZabbixToItop.Models;

namespace ZabbixToItop.Services
{
    public class Itop : IItop
    {
        private string itop_url;
        private string itop_user;
        private string itop_pwd;
        private readonly Utils utils;

        public Itop()
        {
            itop_url = ConfigurationManager.AppSettings["url"];
            itop_user = ConfigurationManager.AppSettings["auth_user"];
            itop_pwd = ConfigurationManager.AppSettings["auth_pwd"];
            utils = new Utils();
        }

        public string GenerateTicket(ItopConfiguration config)
        {
            TicketFields fields = new TicketFields
            {
                Service_id = "SELECT Service WHERE name='" + config.Service_name + "'",
                Servicesubcategory_id = "SELECT ServiceSubcategory JOIN Service ON ServiceSubcategory.service_id = Service.id WHERE ServiceSubcategory.name='" + config.Service_subcategory_name + "' AND Service.name='" + config.Service_name + "'",
                Origin = config.origin,
                contacts_list = new List<string> { config.team },
                Team_id = "SELECT Team WHERE name = '" + config.team + "'", 
                Caller_id = new Caller
                {
                    First_name = "Processo",
                    Name = "Automatico"
                },
                Description = config.description,
                Org_id = "SELECT o FROM FunctionalCI AS fc JOIN Organization AS o ON fc.org_id = o.id WHERE fc.name='" + config.ci + "'",
                Title = config.title, 
                Functionalcis_list = new List<Functionalcis>
                {
                    new Functionalcis
                    {
                        Functionalci_id = "SELECT FunctionalCI WHERE name='" + config.ci + "'",  
                        Impact_code = "manual"
                    }
                },
                Urgency = config.urgency, 
                Impact = config.Impact
            };

            Console.WriteLine(config.resource_group_name);

            if(config.resource_group_name != null){
                fields.Private_log = new ItemsList
                {
                    Items = new List<Item>
                    {
                        new Item
                        {
                            Date =  DateTime.Now,
                            Message = "Resource Group: " + config.resource_group_name + ""
                        }
                    }
                }; 
            }
            
            Ticket ticket = new Ticket
            {
                Class = config.Class, 
                Status = config.Status,
                Comment = config.Comment, 
                Operation = "core/create",
                Output_fields = "id",
                Fields = fields
            };

            return utils.ObjectToJson(ticket);
        }

        public async Task<string> SaveTicketOnItopAsync(string jsonString)
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
            };

            var client = new HttpClient(httpClientHandler);

            var values = new Dictionary<string, string>
            {
                { "auth_pwd", itop_pwd },
                { "auth_user", itop_user },
                { "json_data", jsonString }
            };

            var requestBody = new FormUrlEncodedContent(values);
                    
            var response = await client.PostAsync(itop_url, requestBody);
            
            var itopResponse = utils.FormatItopResponse(await response.Content.ReadAsStringAsync());

            return "code:" + itopResponse.code + " message:" + itopResponse.message;
        }
    }
}
