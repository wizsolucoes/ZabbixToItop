using System;
using ZabbixToItop.Services;
namespace ZabbixToItop.Models
{
    public class ItopConfiguration
    {
        public ItopConfiguration(string[] args)
        {
            Class = args[5];  
            Description = args[6];
            Origin = args[7];
            Ci = args[8];
            Urgency = args[9];
            Team = args[10];
            Title = Ci;
            Impact = args[11];
            Service_name = args.Length < 13 ? null : args[12] == "" ? null : args[12];
            Service_subcategory_name = args.Length < 14 ? null : args[13] == "" ? null : args[13];
            Resource_group_name = args.Length < 15 ? null : args[14] == "" ? null : args[14];
            Status = "dispatched";
            Comment = Ci;
        }

        public void LogConfiguration()
        {
            Log.WriteText("Class = " + Class);
            Log.WriteText("Description = " + Description);
            Log.WriteText("Origin = " + Origin);
            Log.WriteText("CI = " + Ci);
            Log.WriteText("Urgency = " + Urgency);
            Log.WriteText("Team = " + Team);
            Log.WriteText("Impact = " + Impact);
            Log.WriteText("Service_name = " + Service_name);
            Log.WriteText("Service_subcategory_name = " + Service_subcategory_name);
            Log.WriteText("Resource_group_name = " + Resource_group_name);
        }

        private string CheckUrgency(string urgency)
        {
            if (urgency.Equals("Not classified"))
            {
                urgency = "";
            }
            else if(urgency.Equals("Information"))
            {
                urgency = "";
            }
            else if(urgency.Equals("Warning"))
            {
                urgency = "";
            }
            else if(urgency.Equals("Average"))
            {
                urgency = "";
            }
            else if(urgency.Equals("High"))
            {
                urgency = "";
            }
            else if(urgency.Equals("Disaster"))
            {
                urgency = "";
            }
            return urgency;
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
