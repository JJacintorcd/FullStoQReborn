using Recodme.RD.FullStoQReborn.DataAccessLayer.Contexts;
using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using Recodme.RD.FullStoQReborn.DataLayer.Person;
using Recodme.RD.FullStoQReborn.DataLayer.Queue;
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
            var cat1 = new Category("Non-Alcoholic Beverages");
            var bra1 = new Brand("Dona Edite");
            var prof1 = new Profile(123456789, "Paulo", "Macabres", 919191919, DateTime.UtcNow);
            var sb1 = new ShoppingBasket(prof1.Id);
            var mod1 = new ProductModel("Vinho Tinto da Barraca do Tejo", false, "506-1237-424", 3.99, 0.75, bra1.Id, cat1.Id);
            var stoQ1 = new StoreQueue(42, est1.Id);
            var resQ1 = new ReservedQueue(est1.Id, prof1.Id);

            _ctx.Regions.AddRange(reg1);
            _ctx.Companies.AddRange(comp1);
            _ctx.Establishments.AddRange(est1);
            _ctx.Categories.AddRange(cat1);
            _ctx.Brands.AddRange(bra1);
            _ctx.Profiles.AddRange(prof1);
            _ctx.ShoppingBaskets.AddRange(sb1);
            _ctx.ProductModels.AddRange(mod1);
            _ctx.StoreQueues.AddRange(stoQ1);
            _ctx.ReservedQueues.AddRange(resQ1);

            _ctx.SaveChanges();
        }
    }
}
