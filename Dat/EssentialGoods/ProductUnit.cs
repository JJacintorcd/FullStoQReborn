using Recodme.RD.FullStoQReborn.Data.Base;
using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods
{
    public class ProductUnit : Entity
    {
        private string _serialNumber;
        public string SerialNumber
        {
            get => _serialNumber;
            set
            {
                _serialNumber = value;
                RegisterChange();
            }
        }

        private bool _isReserved;
        public bool IsReserved
        {
            get => _isReserved;
            set
            {
                _isReserved = value;
                RegisterChange();
            }
        }


        private Guid _productModel;

        [ForeignKey("ProductModel")]
        [Display(Name = "Product Model")]
        public Guid ProductModelId
        {
            get => _productModel;
            set
            {
                _productModel = value;
                RegisterChange();
            }
        }
        public virtual ProductModel ProductModel { get; set; }


        private Guid _establishmentId;

        [ForeignKey("Region")]
        public Guid EstablishmentId
        {
            get => _establishmentId;
            set
            {
                _establishmentId = value;
                RegisterChange();
            }
        }
        public virtual Establishment Establishment { get; set; }

        private Guid _shoppingBasket;

        [ForeignKey("ShoppingBasket")]
        [Display(Name = "Shopping Basket")]
        public Guid ShoppingBasketId
        {
            get => _shoppingBasket;
            set
            {
                _shoppingBasket = value;
                RegisterChange();
            }
        }
        public virtual ShoppingBasket ShoppingBasket { get; set; }


        public ProductUnit(string serialNumber, bool isReserved, Guid productModelId, Guid establishmentId, Guid shoppingBasketId) : base()
        {
            _serialNumber = serialNumber;
            _isReserved = isReserved;
            ProductModelId = productModelId;
            EstablishmentId = establishmentId;
            ShoppingBasketId = shoppingBasketId;
        }

        public ProductUnit(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, string serialNumber, bool isReserved, Guid productModelId, Guid establishmentId, Guid shoppingBasketId) : base(id, createdAt, updatedAt, isDeleted)
        {
            _serialNumber = serialNumber;
            _isReserved = isReserved;
            ProductModelId = productModelId;
            EstablishmentId = establishmentId;
            ShoppingBasketId = shoppingBasketId;
        }
    }
}
