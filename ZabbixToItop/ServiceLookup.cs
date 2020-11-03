using System.Collections.Generic;

namespace ZabbixToItop
{
    public class ServiceLookup
    {
        public static readonly Dictionary<string, string[]> CiServices = new Dictionary<string,string[]> {
            {"ci1", new string[]{"PageBla.aspx", ""}},
            {"ci2", new string[]{"Page21231.aspx", ""}},
            {"Cluster1", new string[]{"Software", "Microsoft Office Support"}},
            {"Cluster2", new string[]{"teste", "testando"}}
        };
    }
}