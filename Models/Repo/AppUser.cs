using Microsoft.AspNetCore.Identity;

namespace MoviesMafia.Models.Repo
{
    public class AppUser : IdentityUser, IBaseEntity
    {
        public string ProfilePicturePath { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }

}
