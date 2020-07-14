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
            var cat1 = new Category("Non-Alcoholic Beverages");
            var prof1 = new Profile(123456789, "Paulo", "Macabres", 919191919, DateTime.UtcNow);

            var mod1 = new ProductModel("Vinho Tinto da Barraca do Tejo", false, "506-1237-424", 3.99, 0.75);

            _ctx.Regions.AddRange(reg1);
            _ctx.Companies.AddRange(comp1);
            _ctx.Categories.AddRange(cat1);
            _ctx.Profiles.AddRange(prof1);




            _ctx.SaveChanges();
        }
    }
}
