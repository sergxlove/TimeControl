using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeControl.Core.Models;
using TimeControl.DataAccess.Sqlite.Models;

namespace TimeControl.DataAccess.Sqlite.Configurations
{
    public class TargetsConfigurations : IEntityTypeConfiguration<TargetsEntity>
    {
        public void Configure(EntityTypeBuilder<TargetsEntity> builder)
        {
            builder.ToTable("Targets");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired();
            builder.Property(x => x.DateWork)
                .IsRequired();
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(Targets.MAX_LENGTH_DESCRIPTION);
            builder.Property(x => x.DurationMinutes)
                .IsRequired();
            builder.Property(x => x.DoneDurationMinutes)
                .IsRequired();
            builder.Property(x => x.IsDone)
                .IsRequired();
        }
    }
}
