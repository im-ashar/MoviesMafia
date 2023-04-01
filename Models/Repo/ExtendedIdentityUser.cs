using Microsoft.AspNetCore.Identity;

namespace MoviesMafia.Models.Repo
{
    public class ExtendedIdentityUser : IdentityUser
    {
        public string ProfilePicturePath { get; set; }
    }
}
