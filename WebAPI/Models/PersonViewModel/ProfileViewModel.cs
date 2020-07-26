using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
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
                Id = profile.Id
            };
        }

        public Profile ToProfile()
        {
            return new Profile(VatNumber, FirstName, LastName, PhoneNumber, BirthDate);
        }

        public Profile ToModel()
        {
            return new Profile(VatNumber, FirstName, LastName, PhoneNumber, BirthDate);
        }
        public Profile ToModel(Profile model)
        {
            model.BirthDate = BirthDate;
            model.FirstName = FirstName;
            model.LastName = LastName;
            model.VatNumber = VatNumber;
            model.PhoneNumber = PhoneNumber;
            return model;
        }

        public bool CompareToModel(Profile model)
        {
            return BirthDate == model.BirthDate && FirstName == model.FirstName && LastName == model.LastName && VatNumber == model.VatNumber && PhoneNumber == model.PhoneNumber;
        }
    }
}
