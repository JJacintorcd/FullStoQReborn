using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Base;

namespace WebAPI.Models.EssentialGoodsViewModel
{
    public class ShoppingBasketViewModel : BaseViewModel
    {
        [Display(Name = "Profile")]
        [Required(ErrorMessage = "Select a Profile")]
        public Guid ProfileId { get; set; }
        public static ShoppingBasketViewModel Parse(ShoppingBasket shoppingBasket)
        {
            return new ShoppingBasketViewModel()
            {
                ProfileId = shoppingBasket.ProfileId,
                Id = shoppingBasket.Id
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
