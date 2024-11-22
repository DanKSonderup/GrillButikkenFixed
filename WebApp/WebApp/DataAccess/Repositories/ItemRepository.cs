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
    public class ItemRepository
    {
        public static List<ItemDTO> GetItem(string name)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                return context.Items.Where(r => r.Name == name).Select(r => ItemMapper.Map(r)).ToList();
            }
        }

        public static List<ItemDTO> GetItems()
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                return context.Items.Select(r => ItemMapper.Map(r)).ToList();
            }
        }

        //Add
        public static ItemDTO AddItem(ItemDTO itemDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.Items.Add(ItemMapper.Map(itemDTO));
                context.SaveChanges();
            }
            return itemDTO;
        }

        //Update
        public static ItemDTO EditItem(ItemDTO itemDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                Item dataItem = context.Items.Find(itemDTO.Name);
                ItemMapper.Update(itemDTO, dataItem);
            }
            return itemDTO;
        }

        //Delete
        public static ItemDTO DeleteItem(ItemDTO itemDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.Items.Remove(ItemMapper.Map(itemDTO));
            }
            return itemDTO;
        }
    }
}