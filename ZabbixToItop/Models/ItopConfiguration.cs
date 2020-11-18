using System;
using ZabbixToItop.Services;
namespace ZabbixToItop.Models
{
    public class ItopConfiguration
    {
        public ItopConfiguration(string[] args)
        {
            Log.WriteText("arg zabbix 1=" + args[5]);
            Log.WriteText("arg zabbix 2=" + args[6]);
            Log.WriteText("arg zabbix 3=" + args[7]);
            Log.WriteText("arg zabbix 4=" + args[8]);
            Log.WriteText("arg zabbix 5=" + args[9]);
            Log.WriteText("arg zabbix 6=" + args[10]);
            Log.WriteText("arg zabbix 7=" + args[11]);
            args[8] = "Problem started at 17:30:52 on 2020.11.18\n Problem name: teste novo ping\n Host: Cluster1\n Severity: Disaster\n \n Original problem ID: 1453\n";
            Class = args[5];  
            Description = args[6]; 
            Origin = args[7];
            Ci = Utils.GetStringBetween(args[8], "Host: ", " Severity:");
            Urgency = CheckUrgency(Utils.GetStringBetween(args[8], "Severity: ", " Original"));
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
