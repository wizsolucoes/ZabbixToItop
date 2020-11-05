using System;
using System.Collections.Generic;

namespace ZabbixToItop.Models
{
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