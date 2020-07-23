using Recodme.RD.FullStoQReborn.Data.Base;
using Recodme.RD.FullStoQReborn.DataLayer.Person;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods
{
    public class ShoppingBasket : Entity
    {
        public virtual ICollection<ProductUnit> ProductUnits { get; set; }

        private Guid _profileId;

        [ForeignKey("Profile")]
        public Guid ProfileId
        {
            get => _profileId;
            set
            {
                _profileId = value;
                RegisterChange();
            }
        }
        public virtual Profile Profile { get; set; }

        public ShoppingBasket(Guid profileId) : base()
        {
            ProfileId = profileId;

        }

        public ShoppingBasket(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, Guid profileId)
            : base(id, createdAt, updatedAt, isDeleted)
        { 
            ProfileId = profileId;
        }
    }
}
