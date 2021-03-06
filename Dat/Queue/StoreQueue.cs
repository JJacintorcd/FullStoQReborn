﻿using Recodme.RD.FullStoQReborn.Data.Base;
using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recodme.RD.FullStoQReborn.DataLayer.Queue
{
    public class StoreQueue : Entity
    {
        private int _quantity;

        [Required]
        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                RegisterChange();
            }
        }

        private Guid _establishmentId;

        [ForeignKey("Establishment")]
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

        public StoreQueue(int quantity, Guid establishmentId) : base()
        {
            Quantity = quantity;
            EstablishmentId = establishmentId;
        }

        public StoreQueue(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, int quantity, Guid establishmentId) : base
            (id, createdAt, updatedAt, isDeleted)
        {
            Quantity = quantity;
            EstablishmentId = establishmentId;
        }
    }
}
