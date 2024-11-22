using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.DTO;
using WebApp.Models;

namespace WebApp.Service
{
    internal interface IItemDeliveryService
    {
        List<ItemDeliveryDTO> GetItemByNme(int id);
        List<ItemDeliveryDTO> GetAllItems();
        ItemDeliveryDTO CreateItem(int id, List<Item> items, DateTime date);
        ItemDeliveryDTO UpdateItem(ItemDeliveryDTO itemDeliveryDTO);
        ItemDeliveryDTO DeleteItem(ItemDeliveryDTO itemDeliveryDTO);
    }
}
