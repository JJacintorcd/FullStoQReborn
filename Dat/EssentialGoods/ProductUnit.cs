﻿using Recodme.RD.FullStoQReborn.Data.Base;
using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
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

        [ForeignKey("ProductModel")]
        public Guid ProductModelId { get; set; }
        public virtual ProductModel ProductModel { get; set; }

        [ForeignKey("Establishment")]
        public Guid EstablishmentId { get; set; }
        public virtual Establishment Establishment { get; set; }



        public ProductUnit(string serialNumber, bool isReserved, Guid productModelId, Guid establishmentId) : base()
        {
            _serialNumber = serialNumber;
            _isReserved = isReserved;
            ProductModelId = productModelId;
            EstablishmentId = establishmentId;
        }

        public ProductUnit(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, string serialNumber, bool isReserved, Guid productModelId, Guid establishmentId) : base(id, createdAt, updatedAt, isDeleted)
        {
            _serialNumber = serialNumber;
            _isReserved = isReserved;
            ProductModelId = productModelId;
            EstablishmentId = establishmentId;
        }
    }
}
