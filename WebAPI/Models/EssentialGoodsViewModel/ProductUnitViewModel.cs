﻿using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Base;

namespace WebAPI.Models.EssentialGoodsViewModel
{
    public class ProductUnitViewModel : BaseViewModel
    {
        [Display(Name = "Serial Number")]
        [Required(ErrorMessage = "Required Field")]
        public string SerialNumber { get; set; }

        [Display(Name = "Is Reserved")]
        public bool IsReserved { get; set; }

        [Display(Name = "Product Model")]
        [Required(ErrorMessage = "Required Field")]
        public Guid ProductModelId { get; set; }

        [Display(Name = "Establishment")]
        [Required(ErrorMessage = "Required Field")]
        public Guid EstablishmentId { get; set; }


        [Display(Name = "Shopping Basket")]
        [Required(ErrorMessage = "Required Field")]
        public Guid ShoppingBasketId { get; set; }


        public static ProductUnitViewModel Parse(ProductUnit productUnit)
        {
            return new ProductUnitViewModel()
            {
                Id = productUnit.Id,
                SerialNumber = productUnit.SerialNumber,
                IsReserved = productUnit.IsReserved,
                ProductModelId = productUnit.ProductModelId,
                EstablishmentId = productUnit.EstablishmentId,
                ShoppingBasketId = productUnit.ShoppingBasketId
            };
        }

        public ProductUnit ToModel()
        {
            return new ProductUnit(SerialNumber, IsReserved, ProductModelId, EstablishmentId, ShoppingBasketId);
        }

        public ProductUnit ToModel(ProductUnit model)
        {
            model.SerialNumber = SerialNumber;
            model.IsReserved = IsReserved;
            model.ProductModelId = ProductModelId;
            model.EstablishmentId = EstablishmentId;
            model.ShoppingBasketId = ShoppingBasketId;
            return model;
        }

        public bool CompareToModel(ProductUnit model)
        {
            return SerialNumber == model.SerialNumber && IsReserved == model.IsReserved && model.ProductModelId == ProductModelId && model.EstablishmentId == EstablishmentId && model.ShoppingBasketId == ShoppingBasketId;
        }
    }
}
