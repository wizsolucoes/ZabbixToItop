namespace ZabbixToItop.Models
{
    public class Caller
    {
        public Caller(){}

        public Caller(string first_name, string name)
        {
            First_name = first_name;
            Name = name;
        }
        public string First_name { get; set; }
        public string Name { get; set; }
    }
}