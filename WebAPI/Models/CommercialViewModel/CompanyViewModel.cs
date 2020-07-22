using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
using WebAPI.Models.Base;

namespace WebAPI.Models.CommercialViewModel
{
    public class CompanyViewModel : NamedViewModel
    {
        public CompanyViewModel() { }
        public long VatNumber { get; set; }

        public static CompanyViewModel Parse(Company company)
        {
            var cvm = new CompanyViewModel
            {
                Id = company.Id,
                Name = company.Name,
                VatNumber = company.VatNumber
            };
            return cvm;
        }

        public Company ToModel()
        {
            return new Company(Name, VatNumber);
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
