using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ItemDelivery
    {
        public int Id { get; set; }
        public List<Item> Items { get; set; }
        public DateTime Date { get; set; }

        public ItemDelivery(int id, List<Item> items, DateTime date)
        {
            this.Id = id;
            this.Items = items;
            this.Date = date;
        }
    }
}