using System.Diagnostics.CodeAnalysis;

namespace ZabbixToItop.Models
{
    [ExcludeFromCodeCoverage]
    public class Caller
    {
        public Caller(){}

        public Caller(string first_name, string name)
        {
            this.First_name = first_name;
            this.Name = name;
        }
        public string First_name { get; set; }
        public string Name { get; set; }
    }
}