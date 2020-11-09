using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ZabbixToItop.Models
{
    [ExcludeFromCodeCoverage]
    public class Functionalcis
    {
        public Functionalcis() {}

        public string Functionalci_id { get; set; }
        public string Functionalci_name { get; set; }
        public string Impact_code { get; set; }
        public string Impact { get; set; }
        public string Friendlyname { get; set; }
        public string Functionalci_id_friendlyname { get; set; }
        public string Functionalci_id_finalclass_recall { get; set; }
        public string Functionalci_id_obsolescence_flag { get; set; }
    }
}
