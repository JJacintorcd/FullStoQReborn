using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Base;

namespace WebAPI.Models.EssentialGoodsViewModel
{
    public class ProductModelViewModel : BaseViewModel
    {
        public string ProductName { get; set; }
        public string BarCode { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
        public Guid BrandId { get; set; }
        public Guid CategoryId { get; set; }
        public static ProductModelViewModel Parse(ProductModel productModel)
        {
            return new ProductModelViewModel()
            {
                ProductName = productModel.ProductName,
                BarCode = productModel.BarCode,
                Price = productModel.Price,
                Weight = productModel.Weight,
                BrandId = productModel.BrandId,
                CategoryId = productModel.CategoryId
            };
        }

        public ProductModel ToProductModel()
        {
            return new ProductModel(ProductName, BarCode, Price, Weight, BrandId, CategoryId);
        }
    }
}
