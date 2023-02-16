using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace MoviesMafia.Models.Repo
{
    public class UserContext : IdentityDbContext<ExtendedIdentityUser>
    {

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
