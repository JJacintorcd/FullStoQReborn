﻿using Recodme.RD.FullStoQReborn.DataAccessLayer.Contexts;
using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
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


            _ctx.Regions.AddRange(reg1);

            _ctx.SaveChanges();
        }
    }
}
