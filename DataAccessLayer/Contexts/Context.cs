using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Properties;
using Recodme.RD.FullStoQReborn.DataLayer.Commercial;

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

    }
}
