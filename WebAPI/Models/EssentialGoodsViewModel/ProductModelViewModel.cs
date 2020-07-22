using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Base;

namespace WebAPI.Models.EssentialGoodsViewModel
{
    public class ProductModelViewModel : NamedViewModel
    {
        public string BarCode { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
        public Guid BrandId { get; set; }
        public Guid CategoryId { get; set; }
        public static ProductModelViewModel Parse(ProductModel productModel)
        {
            return new ProductModelViewModel()
            {
                Name = productModel.Name,
                BarCode = productModel.BarCode,
                Price = productModel.Price,
                Weight = productModel.Weight,
                BrandId = productModel.BrandId,
                CategoryId = productModel.CategoryId,
                Id = productModel.Id
            };
        }

        public ProductModel ToProductModel()
        {
            return new ProductModel(Name, BarCode, Price, Weight, BrandId, CategoryId);
        }

        public ProductModel ToModel(ProductModel model)
        {
            model.Name = Name;
            model.BarCode = BarCode;
            model.Price = Price;
            model.Weight = Weight;
            model.BrandId = BrandId;
            model.CategoryId = CategoryId;
            return model;
        }

        public bool CompareToModel(ProductModel model)
        {
            return Name == model.Name && BarCode == model.BarCode && Price == model.Price && Weight == model.Weight && BrandId == model.BrandId && CategoryId == model.CategoryId;
        }
    }
}
