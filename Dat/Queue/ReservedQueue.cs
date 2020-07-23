using Recodme.RD.FullStoQReborn.Data.Base;
using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
using Recodme.RD.FullStoQReborn.DataLayer.Person;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recodme.RD.FullStoQReborn.DataLayer.Queue
{
    public class ReservedQueue : Entity
    {
        private Guid _establishmentId;

        [ForeignKey("Establishment")]
        [Display(Name = "Establishment")]
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
        public virtual Profile Profiles { get; set; }

        public ReservedQueue(Guid establishmentId, Guid profileId) : base()
        {
            EstablishmentId = establishmentId;
            ProfileId = profileId;
        }

        public ReservedQueue(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted,
            Guid establishmentId, Guid profileId) : base(id, createdAt, updatedAt, isDeleted)
        {
            EstablishmentId = establishmentId;
            ProfileId = profileId;
        }

    }
}

