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
                VatNumber = company.VatNumber
            };
        }
    }
}
