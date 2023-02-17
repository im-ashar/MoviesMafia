using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
namespace MoviesMafia.Models.GenericRepo
{
    public class RecordsDBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public RecordsDBContext()
        {
        }

        public DbSet<Records> Records { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=MoviesMafiaRecords;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
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
