using System;
using System.Collections.Generic;
using System.Text;

namespace ZabbixToItop.Models
{
    public class ItopConfiguration
    {
        public ItopConfiguration(string[] args)
        {
            Class = args[0];
            description = args[1];
            origin = args[2];
            resource_group_name = args[3];
            ci = args[4];
            urgency = args[5];
            team = args[6];
            title = ci;
            Impact = args[7];
            Service_name = args[8];
            Service_subcategory_name = args[9];
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
