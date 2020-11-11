using System;
using ZabbixToItop.Services;
namespace ZabbixToItop.Models
{
    public class ItopConfiguration
    {
        public ItopConfiguration(string[] args)
        {
            Log.WriteText("arg0 = " + args[0]);
            Log.WriteText("arg1 = " + args[1]);
            Log.WriteText("arg2 = " + args[2]);
            Log.WriteText("arg3 = " + args[3]);
            Class = args[4]; 
            Description = args[5];
            Origin = args[6];
            Ci = args[7];
            Urgency = args[8];
            Team = args[9];
            Title = Ci;
            Impact = args[10];
            Service_name = args.Length < 12 ? null : args[11] == "" ? null : args[11];
            Service_subcategory_name = args.Length < 13 ? null : args[12] == "" ? null : args[12];
            Resource_group_name = args.Length < 14 ? null : args[13] == "" ? null : args[13];
            Status = "dispatched";
            Comment = Ci;
            LogConfiguration();
        }

        private void LogConfiguration()
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
