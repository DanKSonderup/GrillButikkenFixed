using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DataAccess.Repositories;
using WebApp.DTO;
using WebApp.Models;

namespace WebApp.Service
{
    public class ItemService
    {
       public static ItemDTO CreateItem(string name, string category, int count)
        {
            return ItemRepository.AddItem(new ItemDTO(name, category, count));
        }

        public static ItemDTO DeleteItem(ItemDTO itemDTO)
        {
            return ItemRepository.DeleteItem(itemDTO);
        }
        
        public static List<ItemDTO> GetAllItems()
        {
            return ItemRepository.GetItems();
        }

        public static List<ItemDTO> GetItemByName(string name)
        {
            return ItemRepository.GetItem(name);
        }

        public static ItemDTO UpdateItem(ItemDTO itemDTO)
        {
            return ItemRepository.EditItem(itemDTO);
        }

    }
}