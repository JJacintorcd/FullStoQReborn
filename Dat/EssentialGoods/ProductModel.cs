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
        private string _barCode;
        [Required]
        [Display(Name = "Bar Code")]
        public string BarCode
        {
            get => _barCode;
            set
            {
                _barCode = value;
                RegisterChange();
            }
        }

        private string _description;
        [Required]
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
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

        private double _amount;
        [Required]
        public double Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                RegisterChange();
            }
        }

        private Measure _measure;
        public Measure Measure
        {
            get => _measure;
            set
            {
                _measure = value;
                RegisterChange();
            }
        }


        private Guid brandId;

        [Required]
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

        [Required]
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


        public ProductModel(string name, string barCode, string description, double price, double amount, Measure measure, Guid brandId, Guid categoryId) : base(name)
        {
            _barCode = barCode;
            _description = description;
            _price = price;
            _amount = amount;
            _measure = measure;
            BrandId = brandId;
            CategoryId = categoryId;
        }

        public ProductModel(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, string name, string barCode, string description, double price, double amount, Measure measure, Guid brandId, Guid categoryId) : base(id, createdAt, updatedAt, isDeleted, name)
        {
            _barCode = barCode;
            _description = description;
            _price = price;
            _amount = amount;
            _measure = measure;
            BrandId = brandId;
            CategoryId = categoryId;
        }
    }
}
