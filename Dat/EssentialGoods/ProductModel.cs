using Recodme.RD.FullStoQReborn.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods
{
    public class ProductModel : NamedEntity
    {
        [Required]
        [Display (Name="Bar Code")]
        private string _barCode;
        public string BarCode
        {
            get => _barCode;
            set
            {
                _barCode = value;
                RegisterChange();
            }
        }

        [Required]
        private double _price;
        public double Price
        {
            get => _price;
            set
            {
                _price = value;
                RegisterChange();
            }
        }

        [Required]
        private double _weight;
        public double Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                RegisterChange();
            }
        }


        private Guid brandId;

        [ForeignKey("Brand")]
        public Guid BrandId
        {
            get => brandId;
            set
            {
                brandId = value;
                RegisterChange();
            }
        }
        public virtual Brand Brand { get; set; }

        private Guid _categoryId;

        [ForeignKey("Category")]
        public Guid CategoryId
        {
            get => _categoryId;
            set
            {
                _categoryId = value;
                RegisterChange();
            }
        }
        public virtual Category Category { get; set; }

        public virtual ICollection<ProductUnit> ProductUnits { get; set; }


        public ProductModel(string name, string barCode, double price, double weight, Guid brandId, Guid categoryId) : base(name)
        {
            _barCode = barCode;
            _price = price;
            _weight = weight;
            BrandId = brandId;
            CategoryId = categoryId;
        }

        public ProductModel(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, string name, string barCode, double price, double weight, Guid brandId, Guid categoryId) : base(id, createdAt, updatedAt, isDeleted, name)
        {
            _barCode = barCode;
            _price = price;
            _weight = weight;
            BrandId = brandId;
            CategoryId = categoryId;
        }
    }
}
