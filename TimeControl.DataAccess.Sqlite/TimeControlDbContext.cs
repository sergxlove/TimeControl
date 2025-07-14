using Microsoft.EntityFrameworkCore;
using TimeControl.DataAccess.Sqlite.Configurations;
using TimeControl.DataAccess.Sqlite.Models;

namespace TimeControl.DataAccess.Sqlite
{
    public class TimeControlDbContext : DbContext
    {
        public TimeControlDbContext(DbContextOptions<TimeControlDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<NotesWorkEntity> NotesWork { get; set; }

        public DbSet<TargetsEntity> Targets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NotesWorkConfigurations());
            modelBuilder.ApplyConfiguration(new TargetsConfigurations());
            base.OnModelCreating(modelBuilder);
        }
    }
}
