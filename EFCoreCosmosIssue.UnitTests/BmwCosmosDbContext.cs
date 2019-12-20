using EFCoreCosmosIssue.UnitTests.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCosmosIssue.UnitTests
{
    public sealed class BmwCosmosDbContext : DbContext
    {
        public DbSet<BmwCar> BmwCars { get; private set; }

        public BmwCosmosDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("CarsSample");
            
            var bmwCarConfig = new BmwCarConfig();
            bmwCarConfig.Configure(modelBuilder.Entity<BmwCar>());

            var bmwEngineInfoConfig = new BmwEngineInfoConfig();
            bmwEngineInfoConfig.Configure(modelBuilder.Entity<BmwEngineInfo>());
        }
    }
}