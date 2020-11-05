using System;
using System.Collections.Generic;
using System.Text;

namespace ZabbixToItop.Models
{
    public class TicketFields
    {
        public TicketFields()
        {
        }

        public TicketFields(ItopConfiguration config)
        {
            Service_id = "SELECT Service WHERE name='" + config.Service_name + "'";
            Servicesubcategory_id = "SELECT ServiceSubcategory JOIN Service ON ServiceSubcategory.service_id = Service.id WHERE ServiceSubcategory.name='" + config.Service_subcategory_name + "' AND Service.name='" + config.Service_name + "'";
            Origin = config.Origin;
            Contacts_list = new List<string> { config.Team };
            Team_id = "SELECT Team WHERE name = '" + config.Team + "'";
            Caller_id = new Caller("Processo", "Automatico");
            Description = config.Description;
            Org_id = "SELECT o FROM FunctionalCI AS fc JOIN Organization AS o ON fc.org_id = o.id WHERE fc.name='" + config.Ci + "'";
            Title = config.Title;
            Functionalcis_list = new List<Functionalcis>
            {
                new Functionalcis
                {
                    Functionalci_id = "SELECT FunctionalCI WHERE name='" + config.Ci + "'",
                    Impact_code = "manual"
                }
            };
            Urgency = config.Urgency;
            Impact = config.Impact;

            if (config.Service_name == null)
            {
                Service_id = "SELECT Service AS serv JOIN lnkFunctionalCIToService AS lnk ON lnk.service_id = serv.id WHERE functionalci_id_friendlyname = '" + config.Ci + "'";
                Servicesubcategory_id = "SELECT ServiceSubcategory JOIN Service ON ServiceSubcategory.service_id = Service.id WHERE ServiceSubcategory.name='Microsoft Office Support' AND Service.name='Software'";
            }
            else
            {
                Service_id = "SELECT Service WHERE name='" + config.Service_name + "'";
                Servicesubcategory_id = "SELECT ServiceSubcategory JOIN Service ON ServiceSubcategory.service_id = Service.id WHERE ServiceSubcategory.name='" + config.Service_subcategory_name + "' AND Service.name='" + config.Service_name + "'";
            }

            if (config.Resource_group_name != null)
            {
                Private_log = new ItemsList(config.Resource_group_name);
            }
        }

        public string Org_id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Functionalcis> Functionalcis_list { get; set; }
        public string Team_id { get; set; }
        public string Impact { get; set; }
        public string Urgency { get; set; }
        public string Origin { get; set; }
        public string Service_id { get; set; }
        public string Servicesubcategory_id { get; set; }
        public List<string> Contacts_list { get; set; }
        public Caller Caller_id { get; set; }
        public ItemsList Private_log { get; set; }
    }
}
