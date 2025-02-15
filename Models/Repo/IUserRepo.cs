using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MoviesMafia.Models.Repo
{
    public interface IUserRepo
    {
        Task<IdentityResult> SignUpAsync(UserSignUpModel model);

        Task<Microsoft.AspNetCore.Identity.SignInResult> LogInAsync(UserLogInModel model);

        Task LogoutAsync();

        Task<string?> GetUserEmailAsync(string userName);
        Task<string?> GetUserProfilePictureAsync(string userName);
        Task<AppUser> GetUserAsync(ClaimsPrincipal userName);
        Task<IdentityResult> UpdatePasswordAsync(AppUser user, string currentPassword, string newPassword);
        Task<IList<AppUser>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(AppUser user);
        Task<AppUser> GetUserByIdAsync(string id);
        Task<bool> UpdateEmailAsync(string id, string email);
        Task<bool> VerifyEmailAsync(string email, string token);

        Task<bool> UpdateProfilePictureAsync(IFormFile updatedProfilePicture, string userName);
    }

}
