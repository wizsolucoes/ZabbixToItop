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

        public string Org_id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Functionalcis> Functionalcis_list { get; set; }
        public string Operational_status { get; set;  }
        public string Ref { get; set;  }
        public string Org_name { get; set;  }
        public string Caller_name { get; set;  }
        public string Team_id { get; set;  }
        public string Team_name { get; set;  }
        public string Agent_id { get; set;  }
        public string Agent_name { get; set; }
        public string Status { get; set; }
        public string Impact { get; set; }
        public int? Priority { get; set; }
        public string Urgency { get; set; }
        public string Origin { get; set; }
        public string Service_id { get; set; }
        public string Service_name { get; set; }
        public string Servicesubcategory_id { get; set; }
        public string Servicesubcategory_name { get; set; }
        public string Escalation_flag { get; set; }
        public string Escalation_reason { get; set; }
        public string Resolution_code { get; set; }
        public List<string> contacts_list { get; set; }
        public Caller Caller_id { get; set; }
        public ItemsList Private_log { get; set; }
    }
}
