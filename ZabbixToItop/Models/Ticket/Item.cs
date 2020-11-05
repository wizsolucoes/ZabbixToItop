using System;
using System.Diagnostics.CodeAnalysis;

namespace ZabbixToItop.Models
{
    [ExcludeFromCodeCoverage]
    public class Item
    {
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }
}