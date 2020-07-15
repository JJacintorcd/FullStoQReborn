using Recodme.RD.FullStoQReborn.Data.Base;
using System;
using System.Collections.Generic;
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

        [ForeignKey("ProductModel")]
        public Guid ProductModelId { get; set; }
        public virtual ProductModel ProductModel { get; set; }


        public ProductUnit(string serialNumber, Guid productModelId) : base()
        {
            _serialNumber = serialNumber;
            ProductModelId = productModelId;
        }

        public ProductUnit(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, string serialNumber, Guid productModelId) : base(id, createdAt, updatedAt, isDeleted)
        {
            _serialNumber = serialNumber;
            ProductModelId = productModelId;
        }
    }
}
