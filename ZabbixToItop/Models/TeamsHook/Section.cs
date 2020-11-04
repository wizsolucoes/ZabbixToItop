using System.Collections.Generic;
namespace ZabbixToItop.Models
{
    public class Section
    {
        public Section(){}
        
        public string ActivitySubtitle { get; set; }
        public string ActivityTitle { get; set; }
        public List<Fact> Facts { get; set; }
        public bool Markdown { get; set; }
    }
}