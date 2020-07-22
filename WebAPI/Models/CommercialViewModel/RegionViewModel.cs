using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
using System;
using WebAPI.Models.Base;

namespace WebAPI.Models.CommercialViewModel
{
    public class RegionViewModel:NamedViewModel
    {      
        public RegionViewModel() { }

        public Region ToModel()
        {
            return new Region(Name);
        }

        public Region ToModel(Region model)
        {
            model.Name = Name;
            return model;
        }

        public static RegionViewModel Parse(Region region)
        {
            var rvm = new RegionViewModel
            {                
                Name = region.Name,
                Id = region.Id
            };
            return rvm;
        }

        public bool CompareToModel(Region model)
        {
            return Name == model.Name;
        }
    }
}

