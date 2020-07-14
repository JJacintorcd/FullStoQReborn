using Recodme.RD.FullStoQReborn.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.RD.FullStoQReborn.DataLayer.Person
{
    public class Profile : Entity
    {
        private long _vatNumber;

        [Required]
        [Display(Name = "VAT")]
        public long VatNumber
        {
            get => _vatNumber;
            set
            {
                _vatNumber = value;
                RegisterChange();
            }
        }

        private string _firstName;

        [Required]
        [Display(Name = "First Name")]
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                RegisterChange();
            }
        }

        private string _lastName;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                RegisterChange();
            }
        }

        private long _phoneNumber;

        [Required]
        [Display(Name = "Phone Number")]
        public long PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                RegisterChange();
            }
        }

        private DateTime _birthDate;

        [Required]
        [Display(Name = "Birthday")]
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                RegisterChange();
            }
        }

        //public ICollection<ReservedQueue> ReservedQueues { get; set; }
        //public ICollection<ShoppingBasket> ShoppingBaskets { get; set; }


        //[ForeignKey("Account")]
        //public Guid AccountId { get; set; }
        //public virtual Account Account { get; set; }


        public Profile(long vatNumber, string firstName, string lastName, long phoneNumber,
            DateTime birthDate/*, Guid accountId*/)
        {
            _vatNumber = vatNumber;
            _firstName = firstName;
            _lastName = lastName;
            _phoneNumber = phoneNumber;
            _birthDate = birthDate;
            //AccountId = accountId;
        }

        public Profile(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, long vatNumber, string firstName, string lastName, long phoneNumber, DateTime birthDate/*, Guid accountId*/) : base(id, createdAt, updatedAt, isDeleted)
        {
            _vatNumber = vatNumber;
            _firstName = firstName;
            _lastName = lastName;
            _phoneNumber = phoneNumber;
            _birthDate = birthDate;
            //AccountId = accountId;
        }
    }
}
