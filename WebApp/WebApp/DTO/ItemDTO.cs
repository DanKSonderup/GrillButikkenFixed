using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class ItemDTO
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Count { get; set; }

        public ItemDTO(string name, string category, int count)
        {
            Name = name;
            Category = category;
            Count = count;
        }
    }
}