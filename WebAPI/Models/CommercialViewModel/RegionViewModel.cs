using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
using System;
using WebAPI.Models.Base;

namespace WebAPI.Models.CommercialViewModel
{
    public class RegionViewModel:NamedViewModel
    {      
        public Region ToRegion()
        {
            return new Region(Name);
        }
        public static RegionViewModel Parse(Region region)
        {
            return new RegionViewModel()
            {                
                Name = region.Name,
                Id = region.Id
            };
        }
    }
}

