using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MoviesMafia.Models;
using System.Security.Claims;

namespace MoviesMafia.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public AccountController(UserManager<IdentityUser> uManager,
        SignInManager<IdentityUser> sManager)

        {
            userManager = uManager;
            signInManager = sManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUp model)
        {

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    ModelState.AddModelError("", "Email already in use");
                    return View();
                }
                user = new IdentityUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (signInManager.IsSignedIn(User))
                    {
                        return RedirectToAction("Movies", "Movies");
                    }
                    object data = "Welcome To MoviesMafia Now You Can Log In";
                    return View("ThankYou", data);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();

        }


        [HttpPost]
        public async Task<IActionResult> Login(UserLogIn model, string returnUrl = null)
        {

            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Movies", "Movies");
                }
                ModelState.AddModelError(string.Empty, "Invalid Username or Password");

            }
            else
            {
                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Movies", "Movies");
                }
                ModelState.AddModelError(string.Empty, "Invalid Username or Password");

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Movies", "Movies");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()

        {
            return View();
        }
        public IActionResult Profile()

        {
            return View();
        }
    }
}
