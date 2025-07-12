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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NotesWorkConfigurations());
            base.OnModelCreating(modelBuilder);
        }
    }
}
