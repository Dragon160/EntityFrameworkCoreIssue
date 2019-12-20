using System.Collections.Generic;
using System.Linq;
using EFCoreCosmosIssue.UnitTests.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EFCoreCosmosIssue.UnitTests
{
    public class EFCoreCosmosTests
    {
        private const string DatabaseName = "EFCoreIssueDB";
        private const string DbUri = "https://localhost:8081";
        private const string DbKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TestWithDatabases(bool isInMemoryDb)
        {
            using (var clean = new BmwCosmosDbContext(new DbContextOptionsBuilder().UseCosmos(DbUri, DbKey, DatabaseName).Options))
            {
                clean.Database.EnsureDeleted();
            }

            var bmw = new BmwCar()
            {
                Age = 19,
                Owner = "Hans",
                EngineInfos = new List<EngineInfo>(new [] {new BmwEngineInfo {HasXDriveOption = true, Producer = new Producer() { Name = "ZFS" } } })
                //EngineInfo = new BmwEngineInfo { HasXDriveOption = true, Producer = new Producer() { Name = "ZFS" } }

            };

            using var writeContext = CreateBmwDbContext(isInMemoryDb);
            writeContext.BmwCars.Add(bmw);
            writeContext.SaveChanges();

            /*
             * Reading leads to an exception: 
             * "System.ArgumentNullException: Value cannot be null. (Parameter 'arguments[3]')"
             *
             */

            var bmmReadContext = CreateBmwDbContext(isInMemoryDb);
            var bmwInDb = bmmReadContext.BmwCars.Find(bmw.Id);
            Assert.NotNull(bmwInDb);
            Assert.Equal(19, bmwInDb.Age);
            Assert.True(((BmwEngineInfo)bmwInDb.EngineInfos.First()).HasXDriveOption);
            //Assert.True(((BmwEngineInfo)bmwInDb.EngineInfo).HasXDriveOption);

        }

        private static BmwCosmosDbContext CreateBmwDbContext(bool isInMemory)
        {
            var optionsBuilder = new DbContextOptionsBuilder();

            if (isInMemory)
                optionsBuilder.UseInMemoryDatabase(DatabaseName);
            else
                optionsBuilder.UseCosmos(DbUri, DbKey, DatabaseName);

            var context = new BmwCosmosDbContext(optionsBuilder.Options);
            context.Database.EnsureCreated();

            return context;
        }
    }
}
