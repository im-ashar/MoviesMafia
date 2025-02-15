using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using MoviesMafia.Models.GenericRepo;
using System.Security.Claims;

namespace MoviesMafia.Models.Repo
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public DbSet<Records> Records { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            userId = userId ?? "Anonymous"; //User is not logged in
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is IBaseEntity baseEntity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        baseEntity.CreatedAt = DateTimeOffset.UtcNow;
                        baseEntity.UpdatedAt = DateTimeOffset.UtcNow;
                        baseEntity.CreatedBy = userId;
                        baseEntity.UpdatedBy = userId;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        baseEntity.UpdatedAt = DateTimeOffset.UtcNow;
                        baseEntity.UpdatedBy = userId;
                    }
                }

            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
