using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;

namespace ZabbixToItop.Models
{
    [ExcludeFromCodeCoverage]
    public class ItopResponse
    {
        public int code { get; set; }
        public string message { get; set; }
    }
}
