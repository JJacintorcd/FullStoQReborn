using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.HomeViewModel
{
    public class SearchViewModel
    {
        public SearchViewModel() { }

        [Display(Name = "Company")]
        public Guid CompanyId { get; set; }

        [Display(Name = "Region")]
        public Guid RegionId { get; set; }

    }
}
