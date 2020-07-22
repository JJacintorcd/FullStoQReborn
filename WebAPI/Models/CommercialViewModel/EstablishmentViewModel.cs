using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
using System;
using System.ComponentModel.DataAnnotations;
using WebAPI.Models.Base;

namespace WebAPI.Models.CommercialViewModel
{
    public class EstablishmentViewModel:BaseViewModel
    {
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Required Field")]
        public string Address { get; set; }

        [Display(Name = "Opening Hours")]
        [Required(ErrorMessage = "Required Field")]
        public string OpeningHours { get; set; }

        [Display(Name = "Closing Hours")]
        [Required(ErrorMessage = "Required Field")]
        public string ClosingHours { get; set; }

        [Display(Name = "Closing Days")]
        [Required(ErrorMessage = "Required Field")]
        public string ClosingDays { get; set; }
        public Guid RegionId { get; set; }
        public Guid CompanyId { get; set; }

        public Establishment ToModel()
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
                ClosingDays = establishment.ClosingDays,
                RegionId = establishment.RegionId,
                CompanyId = establishment.CompanyId,
                Id = establishment.Id
            };
        }
        public Establishment ToModel(Establishment model)
        {
            model.Address = Address;
            model.OpeningHours = OpeningHours;
            model.ClosingHours = ClosingHours;
            model.ClosingDays = ClosingDays;
            model.RegionId = RegionId;
            model.CompanyId = CompanyId;
            //model.Id = Id;
            return model;
        }

        public bool CompareToModel(Establishment model)
        {
            return Address == model.Address &&
            OpeningHours == model.OpeningHours &&
            ClosingHours == model.ClosingHours &&
            ClosingDays == model.ClosingDays &&
            RegionId == model.RegionId &&
            CompanyId == model.CompanyId;
        }
    }
}
