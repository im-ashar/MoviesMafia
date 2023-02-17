using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MoviesMafia.Models.GenericRepo;
using MoviesMafia.Models.Repo;
using System;
using System.Security.Claims;
using System.Text;

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
            await _userRepo.Logout();
            return RedirectToAction("Movies", "Movies");
        }
        [HttpGet]
        public IActionResult Login()
        {
            _userRepo.Logout();
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            _userRepo.Logout();
            return View();
        }
        [Authorize]
        public IActionResult Profile()
        {
            ViewBag.EmailData = _userRepo.GetUserEmail(User.Identity.Name);
            var ProfilePicturePath = _userRepo.GetUserProfilePicture(User.Identity.Name);
            var extension = Path.GetExtension(ProfilePicturePath);
            ViewBag.ProfilePicturePath = "ProfilePictures/" + User.Identity.Name + extension;
            var movie = new GenericRecordsDB<Records>(new RecordsDBContext());
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = movie.GetByUserId(userId);
            var tuple = new Tuple<List<Records>, UpdateUserModel>(list, new UpdateUserModel());

            return View(tuple);
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
            if (HttpContext.Request.Cookies.ContainsKey(cookieNameMovie))
            {
                data = "You Already Have Requested A Movie/Season Try Again After 24 Hours";
            }
            else
            {
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
            }
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
        [HttpPost]
        public async Task<string> UpdateAccount(UpdateUserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userRepo.GetUser(User);
                var updateResult = await _userRepo.UpdatePassword(user.Result, model.CurrentPassword, model.NewPassword);
                if (updateResult.Succeeded)
                {
                    return "Password Updated Successfully";
                }
                else
                {
                    return "Current Password Is Incorrect";
                }
            }
            else
            {
                return "New Password and Confirm Password Do Not Match";
            }
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            var allUsers = _userRepo.GetAllUsers();
            return View(allUsers);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string deleteButton)
        {
            ExtendedIdentityUser del = System.Text.Json.JsonSerializer.Deserialize<ExtendedIdentityUser>(deleteButton);
            var result = await _userRepo.DeleteUser(del);
            var list = await _userRepo.GetAllUsers();
            return RedirectToAction("Admin", list);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(string id)
        {
            var reult = await _userRepo.GetUserById(id);
            return View("EditUser", reult);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmail(string id, string email)
        {
            var result = await _userRepo.UpdateEmail(id, email);
            return RedirectToAction("Admin");
        }
    }
}
