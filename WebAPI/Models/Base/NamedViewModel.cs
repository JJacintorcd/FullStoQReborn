using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Base
{
    public class NamedViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Required Field")]
        public string Name { get; set; }
    }
}
