using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using MoviesMafia.Models.Repo;

namespace MoviesMafia.Models.GenericRepo
{
    public class RecordsDBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public RecordsDBContext(DbContextOptions<RecordsDBContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Records> Records { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=containers-us-west-67.railway.app;Port=7349;Database=railway;User Id=postgres;Password=DYhmU2HXwP5zzPNEkFX0");
        }*/
        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Records && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((Records)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                    ((Records)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;

                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    ((Records)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }
    }
}
