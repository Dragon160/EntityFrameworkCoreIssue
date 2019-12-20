using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCosmosIssue.UnitTests.Entities
{
    public class BmwCar
    {
        public string Owner { get; set; }
        public int Age { get; set; }
        public string Id { get; set; }
        public IReadOnlyList<EngineInfo> EngineInfos { get; set; }
        
        // Works with simple property
        //public EngineInfo EngineInfo { get; set; }

        public BmwCar()
        {
            Id = Guid.NewGuid().ToString();
        }
    }

    public class BmwCarConfig : IEntityTypeConfiguration<BmwCar>
    {
        public void Configure(EntityTypeBuilder<BmwCar> builder)
        {
            builder.OwnsMany(p => p.EngineInfos);
            //builder.OwnsOne(p => p.EngineInfo);
        }
    }
}