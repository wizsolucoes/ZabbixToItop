using System.Collections.Generic;

namespace ZabbixToItop.Models
{
    public class TeamsHook
    {
        public TeamsHook(){}
        
        public List<Section> Sections { get; set; }
        public string Summary { get; set; }
        public string ThemeColor { get; set; }
    }
}