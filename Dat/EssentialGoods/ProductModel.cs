using Recodme.RD.FullStoQReborn.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods
{
    public class ProductModel : Entity
    {
        //"Vinho Tinto da Barraca do Tejo", false, "506-1237-424", 3.99, 0.75, bra1.Id, cat1.Id

        private string _productName;
        public string ProductName
        {
            get => _productName;
            set
            {
                _productName = value;
                RegisterChange();
            }
        }

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

        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<ProductUnit> ProductUnits { get; set; }


        public ProductModel(string productName, string barCode, double price, double weight, Guid brandId, Guid categoryId) : base()
        {
            _productName = productName;
            _barCode = barCode;
            _price = price;
            _weight = weight;
            BrandId = brandId;
            CategoryId = categoryId;
        }

        public ProductModel(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, string productName, string barCode, double price, double weight, Guid brandId, Guid categoryId) : base(id, createdAt, updatedAt, isDeleted)
        {
            _productName = productName;
            _barCode = barCode;
            _price = price;
            _weight = weight;
            BrandId = brandId;
            CategoryId = categoryId;
        }
    }
}
