﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MoviesMafia.Models.Repo
{
    public interface IUserRepo
    {
        [HttpPost]
        Task<IdentityResult> SignUp(UserSignUpModel model);

        [HttpPost]
        Task<Microsoft.AspNetCore.Identity.SignInResult> LogIn(UserLogInModel model);

        [HttpGet]
        Task Logout();

        string GetUserEmail(string userName);
        string GetUserProfilePicture(string userName);
        Task<ExtendedIdentityUser> GetUser(ClaimsPrincipal userName);
        Task<IdentityResult> UpdatePassword(ExtendedIdentityUser user, string currentPassword, string newPassword);
        Task<IList<ExtendedIdentityUser>> GetAllUsers();
        Task<bool> DeleteUser(ExtendedIdentityUser user);
        Task<ExtendedIdentityUser> GetUserById(string id);
        Task<bool> UpdateEmail(string id,string email);
        Task<bool> VerifyEmail(string email, string token);

        bool UpdateProfilePicture(IFormFile updatedProfilePicture, string userName);
    }

}
