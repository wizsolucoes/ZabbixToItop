using System;
using System.Collections.Generic;
using System.Text;
using ZabbixToItop.Models;

namespace ZabbixToItop.Interfaces
{
    public interface IUtils
    {
        public string ObjectToJson(Object obj);
        public ItopResponse FormatItopResponse(string response);
    }
}
