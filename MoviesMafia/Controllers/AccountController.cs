using Microsoft.AspNetCore.Mvc;
using MoviesMafia.Models.Repo;

namespace MoviesMafia.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepo _userRepo;

        public AccountController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepo.SignUp(model);
                if (result.Succeeded)
                {
                    object data = "Welcome To MoviesMafia Now You Can Log In";
                    return View("ThankYou", data);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(UserLogInModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepo.LogIn(model);
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
                var result = await _userRepo.LogIn(model);
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
            _userRepo.Logout();
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
