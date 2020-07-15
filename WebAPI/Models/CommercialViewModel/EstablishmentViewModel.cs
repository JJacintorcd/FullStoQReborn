using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
using System;
using WebAPI.Models.Base;

namespace WebAPI.Models.CommercialViewModel
{
    public class EstablishmentViewModel:BaseViewModel
    {       
        public string Address { get; set; }
        public string OpeningHours { get; set; }
        public string ClosingHours { get; set; }
        public string ClosingDays { get; set; }
        public Guid RegionId { get; set; }
        public Guid CompanyId { get; set; }

        public Establishment ToCompany()
        {
            return new Establishment(Address, OpeningHours, ClosingHours, ClosingDays, RegionId, CompanyId);
        }
        public static EstablishmentViewModel Parse(Establishment establishment)
        {
            return new EstablishmentViewModel()
            {
                Address = establishment.Address,
                OpeningHours = establishment.OpeningHours,
                ClosingHours = establishment.ClosingHours,
                RegionId = establishment.RegionId,
                CompanyId = establishment.CompanyId
            };
        }
    }
}
