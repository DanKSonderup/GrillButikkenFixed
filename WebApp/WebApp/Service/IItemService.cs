using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.DTO;

namespace WebApp.Service
{
    internal interface IItemService
    {
        List<ItemDTO> GetItemByNme(String name);
        List<ItemDTO> GetAllItems();
        ItemDTO CreateItem(string name, string category, int count);
        ItemDTO UpdateItem(ItemDTO itemDTO);
        ItemDTO DeleteItem(ItemDTO itemDTO);
    }
}
