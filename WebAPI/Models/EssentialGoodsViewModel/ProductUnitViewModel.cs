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
        public bool IsReserved { get; set; }
        public Guid ProductModelId { get; set; }
        public Guid EstablishmentId { get; set; }
        public Guid ShoppingBasketId { get; set; }


        public static ProductUnitViewModel Parse(ProductUnit productUnit)
        {
            return new ProductUnitViewModel()
            {
                SerialNumber = productUnit.SerialNumber,
                IsReserved = productUnit.IsReserved,
                ProductModelId = productUnit.ProductModelId,
                EstablishmentId = productUnit.EstablishmentId,
                ShoppingBasketId = productUnit.ShoppingBasketId
            };
        }

        public ProductUnit ToProductUnit()
        {
            return new ProductUnit(SerialNumber, IsReserved, ProductModelId, EstablishmentId, ShoppingBasketId);
        }
    }
}
