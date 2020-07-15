﻿using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Base;

namespace WebAPI.Models.EssentialGoodsViewModel
{
    public class ProductUnitViewModel:NamedViewModel
    {
        public string  SerialNumber { get; set; }
        public Guid ProductModelId { get; set; }

        public ProductUnit ToProductUnit()
        {
            return new ProductUnit(Name, SerialNumber, ProductModelId);
        }

        public static ProductUnitViewModel Parse(ProductUnit productUnit)
        {
            return new ProductUnitViewModel()
            {
                Name = productUnit.Name,
                ProductModelId = productUnit.ProductModelId,
                SerialNumber = productUnit.SerialNumber
            };
        }
    }
}
