using System.Diagnostics.CodeAnalysis;
using ZabbixToItop.Settings;

namespace ZabbixToItop.Models
{
    [ExcludeFromCodeCoverage]
    public class Ticket
    {
        public Ticket() {}

        public Ticket(RequestSettings settings)
        {
            this.Class = settings.Class;
            this.Status = settings.Status;
            this.Comment = settings.Comment;
            this.Operation = "core/create";
            this.Output_fields = "id";
            this.Fields = new TicketFields(settings);
        }

        public string Operation { get; set; }
        public string @Class { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public string Output_fields { get; set; }
        public TicketFields Fields { get; set; }
    }
}
