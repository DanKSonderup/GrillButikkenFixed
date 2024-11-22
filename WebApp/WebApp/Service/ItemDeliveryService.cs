using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DataAccess.Repositories;
using WebApp.DTO;
using WebApp.Models;

namespace WebApp.Service
{
    public class ItemDeliveryService
    {
        public static ItemDeliveryDTO CreateItem(int id, List<Item> items, DateTime date)
        {
            return ItemDeliveryRepository.AddItemDelivery(new ItemDeliveryDTO(id, items, date));
        }

        public static ItemDeliveryDTO DeleteItem(ItemDeliveryDTO itemDeliveryDTO)
        {
            return ItemDeliveryRepository.DeleteItemDelivery(itemDeliveryDTO);
        }

        public static List<ItemDeliveryDTO> GetAllItemDeliveries()
        {
            return ItemDeliveryRepository.GetItemDeliveries();
        }

        public static List<ItemDeliveryDTO> GetItemDeliveryById(int id)
        {
            return ItemDeliveryRepository.GetItemDelivery(id);
        }

        public static ItemDeliveryDTO UpdateItemDelivery(ItemDeliveryDTO itemDeliveryDTO)
        {
            return ItemDeliveryRepository.EditItemDelivery(itemDeliveryDTO);
        }

    }
}