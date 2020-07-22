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

        public Category ToModel()
        {
            return new Category(Name);
        }

        public Category ToModel(Category model)
        {
            model.Name = Name;
            return model;
        }

        public bool CompareToModel(Category model)
        {
            return Name == model.Name;
        }
    }
}
