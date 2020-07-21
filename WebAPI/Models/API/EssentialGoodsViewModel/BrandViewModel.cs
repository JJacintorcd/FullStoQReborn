using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Base;

namespace WebAPI.Models.EssentialGoodsViewModel
{
    public class BrandViewModel : NamedViewModel
    {
        public static BrandViewModel Parse(Brand brand)
        {
            return new BrandViewModel()
            {
                Name = brand.Name
            };
        }

        public Brand ToBrand()
        {
            return new Brand(Name);
        }
    }
}
