using System.Collections.Generic;
using ZabbixToItop.Settings;

namespace ZabbixToItop.Models
{
    public class TicketFields
    {
        public TicketFields() { }

        public TicketFields(RequestSettings settings)
        {
            Origin = settings.Origin;
            Contacts_list = new List<string> { settings.Team };
            Team_id = "SELECT Team WHERE name = '" + settings.Team + "'";
            Caller_id = new Caller("Processo", "Automatico");
            Description = settings.Description;
            Org_id = "SELECT o FROM FunctionalCI AS fc JOIN Organization AS o ON fc.org_id = o.id WHERE fc.name='" + settings.Ci + "'";
            Title = settings.Title;
            Functionalcis_list = new List<Functionalcis>
            {
                new Functionalcis
                {
                    Functionalci_id = "SELECT FunctionalCI WHERE name='" + settings.Ci + "'",
                    Impact_code = "manual"
                }
            };
            Urgency = FindUrgencyNumber(settings.Urgency);
            Impact = settings.Impact;

            if (settings.Service_name == null)
            {
                Service_id = "SELECT Service AS serv JOIN lnkFunctionalCIToService AS lnk ON lnk.service_id = serv.id WHERE functionalci_id_friendlyname = '" + settings.Ci + "'";
            }
            else
            {
                Service_id = "SELECT Service WHERE name='" + settings.Service_name + "'";
            }
            
            if(settings.Service_subcategory_name != null)
            {
                Servicesubcategory_id = "SELECT ServiceSubcategory JOIN Service ON ServiceSubcategory.service_id = Service.id WHERE ServiceSubcategory.name='" + settings.Service_subcategory_name + "' AND Service.name='" + settings.Service_name + "'";
            }

            if (settings.Resource_group_name != null)
            {
                Private_log = new ItemsList(settings.Resource_group_name);
            }
        }

        public string FindUrgencyNumber(string urgency)
        {
            urgency = urgency.Trim();
            if (urgency.Equals("Not classified"))
            {
                urgency = "0";
            }
            else if(urgency.Equals("Information"))
            {
                urgency = "0";
            }
            else if(urgency.Equals("Warning"))
            {
                urgency = "1";
            }
            else if(urgency.Equals("Average"))
            {
                urgency = "2";
            }
            else if(urgency.Equals("High"))
            {
                urgency = "3";
            }
            else if(urgency.Equals("Disaster"))
            {
                urgency = "4";
            }
            return urgency;
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
