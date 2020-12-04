using System.Collections.Generic;
using ZabbixToItop.Settings;

namespace ZabbixToItop.Models
{
    public class TicketFields
    {
        public TicketFields() { }

        public TicketFields(RequestSettings settings)
        {
            this.Origin = settings.Origin;
            this.Contacts_list = new List<string> { settings.Team };
            this.Team_id = "SELECT Team WHERE name = '" + settings.Team + "'";
            this.Caller_id = new Caller("Processo", "Automatico");
            this.Description = settings.Description;
            this.Org_id = "SELECT o FROM FunctionalCI AS fc JOIN Organization AS o ON fc.org_id = o.id WHERE fc.name='" + settings.Ci + "'";
            this.Title = settings.Title;
            this.Functionalcis_list = new List<Functionalcis>
            {
                new Functionalcis
                {
                    Functionalci_id = "SELECT FunctionalCI WHERE name='" + settings.Ci + "'",
                    Impact_code = "manual"
                }
            };
            this.Urgency = FindUrgencyId(settings.Urgency);
            this.Impact = settings.Impact;
            this.Service_id="SELECT Service WHERE name='"+settings.Service_name+"'";
            this.Servicesubcategory_id="SELECT ServiceSubcategory JOIN Service ON ServiceSubcategory.service_id = Service.id WHERE ServiceSubcategory.name='"+settings.Service_subcategory_name+"' AND Service.name='"+settings.Service_name+"'";
        }

        public string FindUrgencyId(string urgency)
        {
            urgency = urgency.Trim();
            if (urgency.Equals("Not classified"))
            {
                urgency = "4";
            }
            else if(urgency.Equals("Information"))
            {
                urgency = "4";
            }
            else if(urgency.Equals("Warning"))
            {
                urgency = "4";
            }
            else if(urgency.Equals("Average"))
            {
                urgency = "3";
            }
            else if(urgency.Equals("High"))
            {
                urgency = "2";
            }
            else if(urgency.Equals("Disaster"))
            {
                urgency = "1";
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
