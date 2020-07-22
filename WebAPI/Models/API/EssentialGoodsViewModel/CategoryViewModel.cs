using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Base;

namespace WebAPI.Models.EssentialGoodsViewModel
{
    public class CategoryViewModel : NamedViewModel
    {
        public static CategoryViewModel Parse(Category category)
        {
            return new CategoryViewModel()
            {
                Name = category.Name,
                Id = category.Id
            };
        }

        public Category ToCategory()
        {
            return new Category(Name);
        }
    }
}
