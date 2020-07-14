using Recodme.RD.FullStoQReborn.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recodme.RD.FullStoQReborn.DataLayer.Commercial
{
    public class Company : NamedEntity
    {
        private long _vatNumber;

        [Required]
        public long VatNumber
        {
            get => _vatNumber;

            set
            {
                _vatNumber = value;
                RegisterChange();

            }

        }

        public virtual ICollection<Establishment> Establishments { get; set; }

        public Company(string name, long vatNumber) : base(name)
        {
            VatNumber = vatNumber;
        }

        public Company(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, string name, long vatNumber) : base(id, createdAt, updatedAt, isDeleted, name)
        {
            VatNumber = vatNumber;
        }
    }
}
