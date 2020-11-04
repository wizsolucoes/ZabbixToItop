using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ZabbixToItop.Models;

namespace ZabbixToItop.Services
{
    public class Itop
    {
        private string Itop_url;
        private string Itop_user;
        private string Itop_pwd;
        private readonly Utils utils;

        public Itop()
        {
            utils = new Utils();
            Itop_url = utils.GenerateAppConfig().Settings["url"].Value; 
            Itop_user = utils.GenerateAppConfig().Settings["auth_user"].Value;
            Itop_pwd = utils.GenerateAppConfig().Settings["auth_pwd"].Value;
        }

        public string GenerateTicket(ItopConfiguration config)
        {
            TicketFields fields = new TicketFields
            {
                Service_id = "SELECT Service WHERE name='" + config.Service_name + "'",
                Servicesubcategory_id = "SELECT ServiceSubcategory JOIN Service ON ServiceSubcategory.service_id = Service.id WHERE ServiceSubcategory.name='" + config.Service_subcategory_name + "' AND Service.name='" + config.Service_name + "'",
                Origin = config.Origin,
                Contacts_list = new List<string> { config.Team },
                Team_id = "SELECT Team WHERE name = '" + config.Team + "'", 
                Caller_id = new Caller
                {
                    First_name = "Processo",
                    Name = "Automatico"
                },
                Description = config.Description,
                Org_id = "SELECT o FROM FunctionalCI AS fc JOIN Organization AS o ON fc.org_id = o.id WHERE fc.name='" + config.Ci + "'",
                Title = config.Title, 
                Functionalcis_list = new List<Functionalcis>
                {
                    new Functionalcis
                    {
                        Functionalci_id = "SELECT FunctionalCI WHERE name='" + config.Ci + "'",  
                        Impact_code = "manual"
                    }
                },
                Urgency = config.Urgency, 
                Impact = config.Impact
            };

            if(config.Resource_group_name != null)
            {
                fields.Private_log = new ItemsList
                {
                    Items = new List<Item>
                    {
                        new Item
                        {
                            Date =  DateTime.Now,
                            Message = "Resource Group: " + config.Resource_group_name + ""
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
                { "auth_pwd", Itop_pwd },
                { "auth_user", Itop_user },
                { "json_data", jsonString }
            };

            var requestBody = new FormUrlEncodedContent(values);
                    
            var response = await client.PostAsync(Itop_url, requestBody);
            
            var itopResponse = utils.FormatItopResponse(await response.Content.ReadAsStringAsync());

            if (itopResponse.code != 0)
            {
                throw new ItopException(itopResponse.message, itopResponse.code);
            }
            
            return "code:" + itopResponse.code + " message:" + itopResponse.message;
        }
    }
}
