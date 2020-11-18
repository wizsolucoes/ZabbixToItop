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
            Ci = Utils.GetStringBetween(args[8], "Host: ", "^M Severity");
            Urgency = CheckUrgency(Utils.GetStringBetween(args[8], "Severity: ", "^M ^M Original"));
            Team = args[10];
            Title = Ci;
            Impact = args[11];
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
        }

        public string CheckUrgency(string urgency)
        {
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
