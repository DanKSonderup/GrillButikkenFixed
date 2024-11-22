using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DataAccess.Context;
using WebApp.DTO;
using WebApp.DTO.Mappers;
using WebApp.Models;

namespace WebApp.DataAccess.Repositories
{
    public class ItemDeliveryRepository
    {
        public static List<ItemDeliveryDTO> GetItemDelivery(int id)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                return context.ItemDeliveries.Where(r => r.Id == id).Select(r => ItemDeliveryMapper.Map(r)).ToList();
            }
        }

        public static List<ItemDeliveryDTO> GetItemDeliveries()
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                return context.ItemDeliveries.Select(r => ItemDeliveryMapper.Map(r)).ToList();
            }
        }

        //Add
        public static ItemDeliveryDTO AddItemDelivery(ItemDeliveryDTO itemDeliveryDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.ItemDeliveries.Add(ItemDeliveryMapper.Map(itemDeliveryDTO));
                context.SaveChanges();
            }
            return itemDeliveryDTO;
        }

        //Update
        public static ItemDeliveryDTO EditItemDelivery(ItemDeliveryDTO itemDeliveryDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                ItemDelivery dataItemDelivery = context.ItemDeliveries.Find(itemDeliveryDTO.Id);
                ItemDeliveryMapper.Update(itemDeliveryDTO, dataItemDelivery);
            }
            return itemDeliveryDTO;
        }

        //Delete
        public static ItemDeliveryDTO DeleteItemDelivery(ItemDeliveryDTO itemDeliveryDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.ItemDeliveries.Remove(ItemDeliveryMapper.Map(itemDeliveryDTO));
            }
            return itemDeliveryDTO;
        }
    }
}