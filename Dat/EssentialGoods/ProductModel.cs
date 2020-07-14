using Recodme.RD.FullStoQReborn.Data.Base;
using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods
{
    public class ProductModel : NamedEntity
    {
        private bool _isReserved;

        [Required]
        public bool IsReserved
        {
            get => _isReserved;
            set
            {
                _isReserved = value;
                RegisterChange();
            }

        }
        private string _barCode;

        [Required]
        public string BarCode
        {
            get => _barCode;
            set
            {
                _barCode = value;
                RegisterChange();
            }
        }

        private double _price;

        [Required]
        public double Price
        {
            get => _price;
            set
            {
                _price = value;
                RegisterChange();
            }
        }

        private double _weight;

        [Required]
        public double Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                RegisterChange();
            }
        }

        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<ProductUnit> ProductUnits { get; set; }

        public ProductModel(string name, bool isReserved, string barCode, double price, double weight, Guid brandId, Guid categoryId)
            : base(name)
        {
            IsReserved = isReserved;
            BarCode = barCode;
            Price = price;
            Weight = weight;
            BrandId = brandId;
            CategoryId = categoryId;
        }

        public ProductModel(Guid id, DateTime createdAt, DateTime ReservedAt, bool isDeleted,
            string name, bool isReserved, string barCode, double price, double weight, Guid brandId, Guid categoryId)
            : base(id, createdAt, ReservedAt, isDeleted, name)
        {
            IsReserved = isReserved;
            BarCode = barCode;
            Price = price;
            Weight = weight;
            BrandId = brandId;
            CategoryId = categoryId;
        }
    }
}
