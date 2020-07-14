using Recodme.RD.FullStoQReborn.DataAccessLayer.Contexts;
using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using Recodme.RD.FullStoQReborn.DataLayer.Person;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recodme.RD.FullStoQReborn.DataAccessLayer.Seeders
{
    public static class ContextSeeder
    {
        public static void Seed()
        {
            using var _ctx = new Context();
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
     
            var reg1 = new Region("Covilhã");
            var comp1 = new Company("pingo ácido", 123456789);
            var est1 = new Establishment("Avenida da liberdade, numero 1029, Lisboa", "09:00", "20:00", "Domingo", reg1.Id, comp1.Id);
            var pro1 = new Profile(123456, "Tess", "Tickle", 4041234404, DateTime.Now);
            var sb1 = new ShoppingBasket(pro1.Id);


            _ctx.Regions.AddRange(reg1);
            _ctx.Companies.AddRange(comp1);
            _ctx.Establishments.AddRange(est1);
            _ctx.Profiles.AddRange(pro1);
            _ctx.ShoppingBaskets.AddRange(sb1);


            _ctx.SaveChanges();
        }
    }
}
