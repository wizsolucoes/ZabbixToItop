using System.Diagnostics.CodeAnalysis;

namespace ZabbixToItop.Models
{
    [ExcludeFromCodeCoverage]
    public class Fact
    {
        public Fact(){}
        
        public string Name { get; set; }
        public string Value { get; set; }
    }
}