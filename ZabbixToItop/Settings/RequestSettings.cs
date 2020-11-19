using ZabbixToItop.Util;

namespace ZabbixToItop.Settings
{
    public class RequestSettings
    {
        public RequestSettings(string[] args)
        {
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
