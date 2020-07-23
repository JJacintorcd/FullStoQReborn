using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Base;

namespace WebAPI.Models.EssentialGoodsViewModel
{
    public class ProductModelViewModel : NamedViewModel
    {
        [Required(ErrorMessage = "Required Field")] 
        public string BarCode { get; set; }
        
        [Required(ErrorMessage = "Required Field")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public Measure Measure { get; set; }

        [Display(Name = "Brand")]
        [Required(ErrorMessage = "Required Field")]
        public Guid BrandId { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Required Field")]
        public Guid CategoryId { get; set; }
        public static ProductModelViewModel Parse(ProductModel productModel)
        {
            return new ProductModelViewModel()
            {
                Name = productModel.Name,
                BarCode = productModel.BarCode,
                Description = productModel.Description,
                Price = productModel.Price,
                Amount = productModel.Amount,
                Measure = productModel.Measure,
                BrandId = productModel.BrandId,
                CategoryId = productModel.CategoryId,
                Id = productModel.Id
            };
        }

        public ProductModel ToModel()
        {
            return new ProductModel(Name, BarCode, Description, Price, Amount, Measure, BrandId, CategoryId);
        }

        public ProductModel ToModel(ProductModel model)
        {
            model.Name = Name;
            model.BarCode = BarCode;
            model.Description = Description;
            model.Price = Price;
            model.Amount = Amount;
            model.Measure = Measure;
            model.BrandId = BrandId;
            model.CategoryId = CategoryId;
            return model;
        }

        public bool CompareToModel(ProductModel model)
        {
            return Name == model.Name && BarCode == model.BarCode && Description == model.Description && Price == model.Price && Amount == model.Amount && Measure == model.Measure && BrandId == model.BrandId && CategoryId == model.CategoryId;
        }
    }
}
