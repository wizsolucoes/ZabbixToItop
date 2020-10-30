using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZabbixToItop.Interfaces
{
    public interface IItop
    {
        Task<string> SaveTicketOnItopAsync(string str);
    }
}
