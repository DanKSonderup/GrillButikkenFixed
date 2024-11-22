using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    /// <summary>
    /// The name is vague (change?), but refers to finished products that are delivered to the shop
    /// </summary>
    public class Item
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Count { get; set; } 

        public Item(string name, string category, int count)
        {
            Name = name;
            Category = category;
            Count = count;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}