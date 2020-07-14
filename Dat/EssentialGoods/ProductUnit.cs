using Recodme.RD.FullStoQReborn.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods
{
    public class ProductUnit : NamedEntity
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

        [ForeignKey("ProductModel")]
        public Guid ProductModelId { get; set; }
        public virtual ProductModel ProductModel { get; set; }


        public ProductUnit(string name, string serialNumber, Guid productModelId) : base(name)
        {
            _serialNumber = serialNumber;
            ProductModelId = productModelId;
        }

        public ProductUnit(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, string name, string serialNumber, Guid productModelId) : base(id, createdAt, updatedAt, isDeleted, name)
        {
            _serialNumber = serialNumber;
            ProductModelId = productModelId;
        }
    }
}
