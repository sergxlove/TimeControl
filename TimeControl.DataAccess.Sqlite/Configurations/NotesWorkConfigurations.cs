using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeControl.Core.Models;
using TimeControl.DataAccess.Sqlite.Models;

namespace TimeControl.DataAccess.Sqlite.Configurations
{
    public class NotesWorkConfigurations : IEntityTypeConfiguration<NotesWorkEntity>
    {
        public void Configure(EntityTypeBuilder<NotesWorkEntity> builder)
        {
            builder.ToTable("NotesWork");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                .IsRequired();
            builder.Property(a => a.DateWork)
                .IsRequired();
            builder.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(NotesWork.MAX_LENGTH_DESCRIPTION);
            builder.Property(a => a.DurationHour)
                .IsRequired();
            builder.Property(a => a.DurationMinute)
                .IsRequired();
        }
    }
}
