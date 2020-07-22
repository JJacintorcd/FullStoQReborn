using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Properties;
using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using Recodme.RD.FullStoQReborn.DataLayer.Person;
using Recodme.RD.FullStoQReborn.DataLayer.Queue;

namespace Recodme.RD.FullStoQReborn.DataAccessLayer.Contexts
{
    public class Context : IdentityDbContext<FullStoqUser, FullStoqRole, int>
    {
        private readonly string _connectionString = string.Empty; 
        
        public Context() : base() { }

        public Context(string cString)
        {
            _connectionString = cString;
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if(_connectionString == string.Empty)
                {
                    optionsBuilder.UseSqlServer(Resources.ConnectionString);
                }
                else
                {
                    optionsBuilder.UseSqlServer(_connectionString);
                }
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Establishment> Establishments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ReservedQueue> ReservedQueues { get; set; }
        public DbSet<ShoppingBasket> ShoppingBaskets { get; set; }
        public DbSet<ProductModel> ProductModels { get; set; }
        public DbSet<ProductUnit> ProductUnits { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<StoreQueue> StoreQueues { get; set; }

        


    }
}
