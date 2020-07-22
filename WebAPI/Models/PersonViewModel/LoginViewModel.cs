using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.PersonViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Input your user name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Input your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
