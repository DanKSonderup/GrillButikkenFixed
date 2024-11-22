using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.DTO
{
    public class ItemDeliveryDTO
    {
        public int Id { get; set; }
        public List<Item> Items { get; set; }
        public DateTime Date { get; set; }

        public ItemDeliveryDTO(int id, List<Item> items, DateTime date)
        {
            this.Id = id;
            this.Items = items;
            this.Date = date;
        }
    }
}