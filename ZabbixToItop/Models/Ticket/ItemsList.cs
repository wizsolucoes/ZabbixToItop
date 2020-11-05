using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ZabbixToItop.Models
{
    [ExcludeFromCodeCoverage]
    public class ItemsList
    {

        public ItemsList() { }
        public ItemsList(string resource_group_name)
        {
            Items = new List<Item>
            {
                new Item
                {
                    Date =  DateTime.Now,
                    Message = "Resource Group: " + resource_group_name + ""
                }
            };
        }
        public List<Item> Items { get; set; }
    }
}