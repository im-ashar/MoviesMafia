using Microsoft.EntityFrameworkCore;

namespace MoviesMafia.Models.GenericRepo
{
    public class RecordsDBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public RecordsDBContext()
        {
        }

        public DbSet<Records> Movies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=MoviesMafiaRecords;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
