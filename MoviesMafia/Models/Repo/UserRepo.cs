using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MoviesMafia.Models.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserRepo(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> sManager)
        {
            _userManager = userManager;
            _signInManager = sManager;
        }

        public async Task<IdentityResult> SignUp(UserSignUpModel model)
        {

            var user = new IdentityUser
            {
                UserName = model.Username,
                Email = model.Email,
                EmailConfirmed = true,
                LockoutEnabled = false

            };
            var checkEmail = await _userManager.FindByEmailAsync(model.Email);
            if (checkEmail != null)
            {
                var result = IdentityResult.Failed(new IdentityError { Code = "0001", Description = "Email Already Exists" });
                return result;
            }
            else
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                return result;
            }
        }
        public async Task<Microsoft.AspNetCore.Identity.SignInResult> LogIn(UserLogInModel model, string returnUrl = null)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            return result;
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
            return;
        }

    }
}
