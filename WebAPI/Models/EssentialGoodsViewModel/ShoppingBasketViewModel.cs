using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using System;

namespace WebAPI.Models.EssentialGoodsViewModel
{
    public class ShoppingBasketViewModel
    {
        public ShoppingBasket ToShoppingBasket()
        {
            return new ShoppingBasket();
        }

        public static ShoppingBasketViewModel Parse(ShoppingBasket shoppingBasket)
        {
            return new ShoppingBasketViewModel()
            {
                Name = productUnit.Name,
                ProductModelId = productUnit.ProductModelId,
                SerialNumber = productUnit.SerialNumber
            };
        }
    }
}
