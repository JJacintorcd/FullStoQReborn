using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
using WebAPI.Models.Base;

namespace WebAPI.Models.CommercialViewModel
{
    public class CompanyViewModel : NamedViewModel
    {
        public long VatNumber { get; set; }

        public Company ToCompany()
        {
            return new Company(Name, VatNumber);
        }
        public static CompanyViewModel Parse(Company company)
        {
            return new CompanyViewModel()
            {
                Name = company.Name,
                VatNumber = company.VatNumber,
                Id = company.Id
            };
        }

        public Company ToModel(Company model)
        {
            model.Name = Name;
            model.VatNumber = VatNumber;
            return model;
        }

        public bool CompareToModel(Company model)
        {
            return Name == model.Name && VatNumber == model.VatNumber;
        }
    }
}
