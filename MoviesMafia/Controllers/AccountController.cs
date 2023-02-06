using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MoviesMafia.Models.GenericRepo;
using MoviesMafia.Models.Repo;
using System.Security.Claims;

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
        [Authorize]
        public IActionResult Profile()
        {
            ViewBag.EmailData = _userRepo.GetUserEmail(User.Identity.Name);
            var movie = new GenericRecordsDB<Records>(new RecordsDBContext());
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = movie.GetByUserId(userId);
            return View(list);
        }
        [Authorize]
        public IActionResult DeleteRecord(string deleteButton)
        {
            ViewBag.EmailData = _userRepo.GetUserEmail(User.Identity.Name);
            Records del = System.Text.Json.JsonSerializer.Deserialize<Records>(deleteButton);
            var delete = new GenericRecordsDB<Records>(new RecordsDBContext());
            delete.Delete(del);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = delete.GetByUserId(userId);
            return RedirectToAction("Profile", list);
        }

        [Authorize]
        public IActionResult RequestMovie(string name, int year, string type)
        {
            object data = string.Empty;
            string cookieNameMovie = "movie_name_" + HttpContext.User.Identity.Name;
            /*if (HttpContext.Request.Cookies.ContainsKey(cookieNameMovie))
            {
                data = "You Already Have Requested A Movie/Season Try Again After 24 Hours";
            }
            else
            {*/
            try
            {

                var record = new GenericRecordsDB<Records>(new RecordsDBContext());
                record.Add(new Records { Name = name, UserId = User.FindFirstValue(ClaimTypes.NameIdentifier), Year = year, Type = type, ModifiedBy = User.FindFirstValue(ClaimTypes.NameIdentifier) });
                data = "Your Requested Movie " + name + " Has Been Received";
                HttpContext.Response.Cookies.Append(cookieNameMovie, name, new CookieOptions { Expires = DateTime.Now.AddDays(1) });
            }
            catch (Exception ex)
            {
                data = ex.Message;
            }
            //}
            return View("ThankYou", data);
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditRecord(int id, string name, int year, string type)
        {
            if (ModelState.IsValid)
            {
                var record = new GenericRecordsDB<Records>(new RecordsDBContext());
                Records r = record.GetById(id);
                r.Name = name; r.Year = year; r.Type = type; r.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); r.ModifiedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                record.Update(r);
            }
            return RedirectToAction("Profile");
        }
        [Authorize]
        public IActionResult EditRecord(int id)
        {
            GenericRecordsDB<Records> record = new GenericRecordsDB<Records>(new RecordsDBContext());
            Records r = record.GetById(id);
            return View("EditRecord", r);
        }
    }
}
