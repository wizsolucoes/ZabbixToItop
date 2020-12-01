using ZabbixToItop.Util;

namespace ZabbixToItop.Settings
{
    public class RequestSettings
    {
        public RequestSettings(string[] args)
        {
            Itop_url = args[0];
            Itop_user = args[1];
            Itop_pwd = args[2];
            Class = args[5];  
            Description = args[6]; 
            Origin = "monitoring";
            Team = Helper.GetStringBetween(args[7], "Equipe:", "Host:");
            Ci = Helper.GetStringBetween(args[7], Team + "Host:", "Severidade:");
            Urgency = Helper.GetStringBetween(args[7], "Severidade:", "Impacto:");
            Title = Ci;
            Impact = Helper.GetStringBetween(args[7], "Impacto:", "");
            Status = "dispatched";
            Comment = Ci;
            Service_name = args[7];
            Service_subcategory_name = args[8];
        }

        public RequestSettings()
        {
        }

        public string Itop_url { get; set; }
        public string Itop_user { get; set; }
        public string Itop_pwd { get; set; }
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
