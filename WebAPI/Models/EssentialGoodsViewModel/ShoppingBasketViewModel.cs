using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.EssentialGoodsViewModel
{
    public class ShoppingBasketViewModel
    {
        public Guid ProfileId { get; set; }
        public static ShoppingBasketViewModel Parse(ShoppingBasket shoppingBasket)
        {
            return new ShoppingBasketViewModel()
            {
                ProfileId = shoppingBasket.ProfileId
            };
        }

        public ShoppingBasket ToShoppingBasket()
        {
            return new ShoppingBasket(ProfileId);
        }
    }
}
