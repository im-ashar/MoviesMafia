using Microsoft.AspNetCore.Identity;

namespace MoviesMafia.Models.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly UserManager<ExtendedIdentityUser> _userManager;
        private readonly SignInManager<ExtendedIdentityUser> _signInManager;

        public UserRepo(UserManager<ExtendedIdentityUser> userManager, SignInManager<ExtendedIdentityUser> sManager)
        {
            _userManager = userManager;
            _signInManager = sManager;
        }

        public async Task<IdentityResult> SignUp(UserSignUpModel model)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", model.ProfilePicture.FileName);

            var user = new ExtendedIdentityUser
            {
                UserName = model.Username,
                Email = model.Email,
                EmailConfirmed = true,
                LockoutEnabled = false,
                ProfilePicturePath = path
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
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.ProfilePicture.CopyTo(stream);
                }
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
        public string GetUserEmail(string userName)
        {
            var email = _userManager.FindByNameAsync(userName).Result.Email;
            return email;
        }
        public string GetUserProfilePicture(string userName)
        {
            var profilePicture = _userManager.FindByNameAsync(userName).Result.ProfilePicturePath;
            return profilePicture;
        }
    }
}
