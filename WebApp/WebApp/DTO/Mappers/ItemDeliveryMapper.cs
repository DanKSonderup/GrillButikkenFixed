using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.DTO.Mappers
{
    public class ItemDeliveryMapper
    {
        public static ItemDeliveryDTO Map(ItemDelivery itemDelivery)
        {
            if (itemDelivery == null) return null;
            else
                return new ItemDeliveryDTO(
                    itemDelivery.Id,
                    itemDelivery.Items,
                    itemDelivery.Date);
        }
        public static ItemDelivery Map(ItemDeliveryDTO itemDeliveryDTO)
        {
            if (itemDeliveryDTO == null) return null;
            else
                return new ItemDelivery(
                    itemDeliveryDTO.Id,
                    itemDeliveryDTO.Items,
                    itemDeliveryDTO.Date);
        }
        internal static void Update(ItemDeliveryDTO itemDeliveryDTO, ItemDelivery itemDelivery)
        {
            if (itemDeliveryDTO == null || itemDelivery == null) return;
            itemDelivery.Id = itemDeliveryDTO.Id;
            itemDelivery.Items = itemDeliveryDTO.Items;
            itemDelivery.Date = itemDeliveryDTO.Date;
        }
    }
}