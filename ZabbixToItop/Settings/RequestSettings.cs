using ZabbixToItop.Util;

namespace ZabbixToItop.Settings
{
    public class RequestSettings
    {
        public RequestSettings(string[] args)
        {
            this.Itop_url = args[0];
            this.Itop_user = args[1];
            this.Itop_pwd = args[2];
            this.Class = args[5];
            this.Description = args[6];
            this.Origin = "monitoring";
            this.Team = Helper.GetStringBetween(args[7], "EQUIPE:", ", IMPACTO:");
            this.Ci = Helper.GetStringBetween(args[7], "Host:", "Severity:");
            this.Urgency = Helper.GetStringBetween(args[7], "Severity:", "EQUIPE:");
            this.Title = this.Ci;
            this.Impact = Helper.GetStringBetween(args[7], "IMPACTO:", ", SERVICE:");
            this.Status = "dispatched";
            this.Comment = this.Ci;
            this.Service_name = Helper.GetStringBetween(args[7], "SERVICE:", ", SERVICESUBCATEGORY:");
            this.Service_subcategory_name = args[7].Split("SERVICESUBCATEGORY:")[1];     
            Log.WriteText("Team = " + this.Team);
            Log.WriteText("Ci = " + this.Ci);
            Log.WriteText("Urgency = " + this.Urgency);
            Log.WriteText("Impact = " + this.Impact);
            Log.WriteText("Service = " + this.Service_name);
            Log.WriteText("Service Subcategory = " + this.Service_subcategory_name);
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
