using System;
using System.Collections.Generic;
using System.Text;
using ZabbixToItop.Models;

namespace ZabbixToItop.Interfaces
{
    public interface IZabbix
    {
        public Ticket GetTicketFromZabbix();
    }
}
