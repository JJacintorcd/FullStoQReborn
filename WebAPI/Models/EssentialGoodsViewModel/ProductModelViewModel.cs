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
        public bool IsReserved { get; set; }
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
                IsReserved = productModel.IsReserved,
                BarCode = productModel.BarCode,
                Price = productModel.Price,
                Weight = productModel.Weight,
                BrandId = productModel.BrandId,
                CategoryId = productModel.CategoryId
            };
        }

        public ProductModel ToProductModel()
        {
            return new ProductModel(Name, IsReserved, BarCode, Price, Weight, BrandId, CategoryId);
        }
    }
}
