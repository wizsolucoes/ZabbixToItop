using System;

namespace ZabbixToItop.Models
{
    public class ItopConfiguration
    {
        public ItopConfiguration(string[] args)
        {
            Class = args[0];
            Description = args[1];
            Origin = args[2];
            Ci = args[3];
            Urgency = args[4];
            Team = args[5];
            Title = Ci;
            Impact = args[6];
            Service_name = args.Length < 8 ? null : args[7] == "" ? null : args[7];
            Service_subcategory_name = args.Length < 8 ? null : args[8] == "" ? null : args[8];
            Resource_group_name = args.Length < 10 ? null : args[9] == "" ? null : args[9];
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
