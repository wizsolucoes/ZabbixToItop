using System;
using System.Collections.Generic;
using System.Text;

namespace ZabbixToItop.Models
{
    public class Ticket
    {
        public Ticket()
        {

        }
        public Ticket(string operation, string @class, string comment, string output_fields, TicketFields fields)
        {
            this.Operation = operation;
            this.Class = @class;
            this.Comment = comment;
            this.Output_fields = output_fields;
            this.Fields = fields;
        }

        public string Operation { get; set; }
        public string @Class { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public string Output_fields { get; set; }
        public TicketFields Fields { get; set; }
    }
}
