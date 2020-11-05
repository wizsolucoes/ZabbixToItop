using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ZabbixToItop.Models
{
    [ExcludeFromCodeCoverage]
    public class TeamsHook
    {
        public TeamsHook(){}
        
        public List<Section> Sections { get; set; }
        public string Summary { get; set; }
        public string ThemeColor { get; set; }
    }
}