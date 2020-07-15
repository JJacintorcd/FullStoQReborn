using Recodme.RD.FullStoQReborn.Data.Base;
using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using Recodme.RD.FullStoQReborn.DataLayer.Queue;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recodme.RD.FullStoQReborn.DataLayer.Commercial
{
    public class Establishment : Entity
    {
        private string _address;

        [Required]
        public string Address
        {
            get => _address;

            set
            {
                _address = value;
                RegisterChange();
            }
        }

        private string _openingHours;

        [Required]
        [Display(Name = "Opening Hours")]
        public string OpeningHours
        {
            get => _openingHours;

            set
            {
                _openingHours = value;
                RegisterChange();
            }
        }

        private string _closingHours;

        [Required]
        [Display(Name = "Closing Hours")]
        public string ClosingHours
        {
            get => _closingHours;

            set
            {
                _closingHours = value;
                RegisterChange();
            }
        }

        private string _closingDays;

        [Required]
        [Display(Name = "Closing Days")]
        public string ClosingDays
        {
            get => _closingDays;

            set
            {
                _closingDays = value;
                RegisterChange();
            }
        }

        [ForeignKey("Region")]
        public Guid RegionId { get; set; }
        public virtual Region Region { get; set; }


        [ForeignKey("Company")]
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public ICollection<ReservedQueue> ReservedQueues { get; set; }
        public ICollection<StoreQueue> StoreQueues { get; set; }
        public virtual ICollection<ProductModel> ProductModels { get; set; }
        public virtual ICollection<ShoppingBasket> ShoppingBaskets { get; set; }


        public Establishment(string address, string openingHours, string closingHours, string closingDays, Guid regionId,
            Guid companyId)
        {
            Address = address;
            OpeningHours = openingHours;
            ClosingHours = closingHours;
            ClosingDays = closingDays;
            RegionId = regionId;
            CompanyId = companyId;

        }

        public Establishment(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted,
            string address, string openingHours, string closingHours, string closingDays, Guid regionId,
            Guid companyId) : base(id, createdAt, updatedAt, isDeleted)
        {
            Address = address;
            OpeningHours = openingHours;
            ClosingHours = closingHours;
            ClosingDays = closingDays;
            RegionId = regionId;
            CompanyId = companyId;
        }

    }
}
