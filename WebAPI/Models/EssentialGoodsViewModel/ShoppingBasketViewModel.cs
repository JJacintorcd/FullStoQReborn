using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Base;

namespace WebAPI.Models.EssentialGoodsViewModel
{
    public class ShoppingBasketViewModel : BaseViewModel
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
        public ShoppingBasket ToModel(ShoppingBasket model)
        {
            model.ProfileId = ProfileId;
            return model;
        }

        public bool CompareToModel(ShoppingBasket model)
        {
            return ProfileId == model.ProfileId;
        }
    }
}
