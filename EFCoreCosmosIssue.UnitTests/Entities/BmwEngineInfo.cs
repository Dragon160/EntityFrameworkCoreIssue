using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCosmosIssue.UnitTests.Entities
{
    public class BmwEngineInfo : EngineInfo
    {
        public bool HasXDriveOption { get; set; }

        public Producer Producer { get; set; }
    }

    public class BmwEngineInfoConfig : IEntityTypeConfiguration<BmwEngineInfo>
    {
        public void Configure(EntityTypeBuilder<BmwEngineInfo> builder)
        {
            builder.OwnsOne(p => p.Producer);
        }
    }
}