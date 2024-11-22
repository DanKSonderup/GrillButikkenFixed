using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.DTO.Mappers
{
    public class ItemMapper
    {
        public static ItemDTO Map(Item item)
        {
            if (item == null) return null;
            else
                return new ItemDTO(
                    item.Name,
                    item.Category,
                    item.Count);
        }
        public static Item Map(ItemDTO itemDTO)
        {
            if (itemDTO == null) return null;
            else
                return new Item(
                    itemDTO.Name,
                    itemDTO.Category,
                    itemDTO.Count);
        }
        internal static void Update(ItemDTO itemDTO, Item item)
        {
            if (itemDTO == null || item == null) return;
            item.Name = itemDTO.Name;
            item.Category = itemDTO.Category;
            item.Count = itemDTO.Count;
        }
    }
}