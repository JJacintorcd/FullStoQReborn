using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Properties;
using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using Recodme.RD.FullStoQReborn.DataLayer.Person;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recodme.RD.FullStoQReborn.DataAccessLayer.Contexts
{
    public class Context : IdentityDbContext
    {
        public Context() : base()
        {

        }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Resources.ConnectionString);
            }
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Company> Companies { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProductModel> Models { get; set; }
        public DbSet<ProductUnit> ProductUnits { get; set; }
        public DbSet<Brand> Brands { get; set; }
        





    }
}
