using System;

namespace ZabbixToItop.Models
{
    public class ItopConfiguration
    {
        public ItopConfiguration(string[] args)
        {
            Class = args[0];
            description = args[1];
            origin = args[2];
            ci = args[3];
            urgency = args[4];
            team = args[5];
            title = ci;
            Impact = args[6];

            if (7 >= args.Length)
            {
                if (ServiceLookup.CiServices.ContainsKey(ci))
                {
                    var serviceLookup = ServiceLookup.CiServices[ci];
                    Service_name = serviceLookup[0];
                    Service_subcategory_name = serviceLookup[1];
                }
                else
                {
                    throw new Exception("Ci não possui service cadastrado no sistema!");
                }
            }
            else
            {
                Service_name = args[7];
                Service_subcategory_name = args[8];
            }

            resource_group_name = 9 < args.Length ? args[9] : null;
            Status = "dispatched";
            Comment = ci;
        }

        public string Service_name { get; set; } 
        public string Service_subcategory_name { get; set; } 
        public string origin { get; set; } 
        public string team { get; set; } 
        public string description { get; set; } 
        public string ci { get; set; } 
        public string title { get; set; } 
        public string urgency { get; set; } 
        public string Impact { get; set; } 
        public string resource_group_name { get; set; } 
        public string @Class { get; set; } 
        public string Status { get; set; }
        public string Comment { get; set; }
    }
}
