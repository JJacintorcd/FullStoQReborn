using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using WebAPI.Models.Base;

namespace WebAPI.Models.EssentialGoodsViewModel
{
    public class BrandViewModel : NamedViewModel
    {
        public BrandViewModel() { }

        public static BrandViewModel Parse(Brand brand)
        {
            var cvm = new BrandViewModel
            {
                Id = brand.Id,
                Name = brand.Name
            };
            return cvm;
        }

        public Brand ToModel()
        {
            return new Brand(Name);
        }

        public Brand ToModel(Brand model)
        {
            model.Name = Name;
            return model;
        }

        public bool CompareToModel(Brand model)
        {
            return Name == model.Name;
        }
    }
}

