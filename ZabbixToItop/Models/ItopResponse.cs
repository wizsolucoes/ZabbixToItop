using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ZabbixToItop.Models
{
    public class ItopResponse
    {
        public int code { get; set; }
        public string message { get; set; }
    }
}
