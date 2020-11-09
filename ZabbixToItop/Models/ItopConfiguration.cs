using System;

namespace ZabbixToItop.Models
{
    public class ItopConfiguration
    {
        public ItopConfiguration(string[] args)
        {
            Class = args[3];
            Description = args[4];
            Origin = args[5];
            Ci = args[6];
            Urgency = args[7];
            Team = args[8];
            Title = Ci;
            Impact = args[9];
            Service_name = args.Length < 11 ? null : args[10] == "" ? null : args[10];
            Service_subcategory_name = args.Length < 11 ? null : args[11] == "" ? null : args[11];
            Resource_group_name = args.Length < 13 ? null : args[12] == "" ? null : args[12];
            Status = "dispatched";
            Comment = Ci;
        }

        public string Service_name { get; set; } 
        public string Service_subcategory_name { get; set; } 
        public string Origin { get; set; } 
        public string Team { get; set; } 
        public string Description { get; set; } 
        public string Ci { get; set; } 
        public string Title { get; set; } 
        public string Urgency { get; set; } 
        public string Impact { get; set; } 
        public string Resource_group_name { get; set; } 
        public string @Class { get; set; } 
        public string Status { get; set; }
        public string Comment { get; set; }
    }
}
