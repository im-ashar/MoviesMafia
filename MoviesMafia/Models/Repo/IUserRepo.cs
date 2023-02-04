﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MoviesMafia.Models.Repo
{
    public interface IUserRepo
    {
        [HttpPost]
        Task<IdentityResult> SignUp(UserSignUpModel model);

        [HttpPost]
        Task<Microsoft.AspNetCore.Identity.SignInResult> LogIn(UserLogInModel model, string returnUrl = null);

        [HttpGet]
        Task Logout();

        string GetUserEmail(string userName);
    }

}