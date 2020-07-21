using Recodme.RD.FullStoQReborn.DataLayer.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Base;

namespace WebAPI.Models.PersonViewModel
{
    public class ProfileViewModel : BaseViewModel
    {
        public long VatNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public static ProfileViewModel Parse(Profile profile)
        {
            return new ProfileViewModel()
            {
                BirthDate = profile.BirthDate,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                VatNumber = profile.VatNumber,
                PhoneNumber = profile.PhoneNumber,
            };
        }

        public Profile ToProfile()
        {
            return new Profile(VatNumber, FirstName, LastName, PhoneNumber, BirthDate);
        }
    }
}
