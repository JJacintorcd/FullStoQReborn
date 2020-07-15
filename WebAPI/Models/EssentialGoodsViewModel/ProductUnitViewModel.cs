using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Base;

namespace WebAPI.Models.EssentialGoodsViewModel
{
    public class ProductUnitViewModel : BaseViewModel
    {
        public string SerialNumber { get; set; }
        public Guid ProductModelId { get; set; }
        public static ProductUnitViewModel Parse(ProductUnit productUnit)
        {
            return new ProductUnitViewModel()
            {
                SerialNumber = productUnit.SerialNumber,
                ProductModelId = productUnit.ProductModelId
            };
        }

        public ProductUnit ToProductUnit()
        {
            return new ProductUnit(SerialNumber, ProductModelId);
        }
    }
}
