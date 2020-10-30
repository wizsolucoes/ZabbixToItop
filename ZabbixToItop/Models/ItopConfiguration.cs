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
            Current_description = args[1];
            Current_origin = args[2];
            //retirar
            Current_resource_group_name = args[3];
            Current_resource_name = args[4];
            Current_urgency = args[5];
            Current_team = args[6];
            Current_title = Current_resource_name;
            Impact = args[7];
            Service_name = args[8];
            Service_subcategory_name = args[9];
            Status = "dispatched";
            Comment = Current_resource_name;
        }

        public string Service_name { get; set; } 
        public string Service_subcategory_name { get; set; } 
        public string Current_origin { get; set; } 
        public string Current_team { get; set; } 
        public string Current_description { get; set; } 
        public string Current_resource_name { get; set; } 
        public string Current_title { get; set; } 
        public string Current_urgency { get; set; } 
        public string Impact { get; set; } 
        public string Current_resource_group_name { get; set; } 
        public string @Class { get; set; } 
        public string Status { get; set; }
        public string Comment { get; set; }
    }
}
