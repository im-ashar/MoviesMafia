﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesMafia.Models.GenericRepo;
using MoviesMafia.Models.Repo;
using System.Security.Claims;

namespace MoviesMafia.Controllers
{

    public class AccountController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly IGenericRecordsDB<Records> _recordsRepo;
        public AccountController(IUserRepo userRepo, IGenericRecordsDB<Records> recordsRepo)
        {
            _userRepo = userRepo;
            _recordsRepo = recordsRepo;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpModel model)
        {

            if (ModelState.IsValid)
            {

                var result = await _userRepo.SignUp(model);
                if (result.Succeeded)
                {
                    object data = "A Confirmation Email Has Been Sent To Your Email Address. Please Check Your Email And Click The Confirmation Link To Activate Your Account.";
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
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError(string.Empty, "In Order To Login You Need To Confirm Your Email. Check Your Email Box.");

                }
                else
                {
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return LocalRedirect(returnUrl);
                        }
                        return RedirectToAction("GetMovie", "Collection");
                    }
                    ModelState.AddModelError(string.Empty, "Invalid Username or Password");
                    
                }
            }
            else
            {
                var result = await _userRepo.LogIn(model);
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError(string.Empty, "In Order To Login You Need To Confirm Your Email. Check Your Email Box.");

                }
                else
                {
                    if (result.Succeeded)
                    {
                        return RedirectToAction("GetMovie", "Collection");
                    }
                    ModelState.AddModelError(string.Empty, "Invalid Username or Password");
                }
            }
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _userRepo.Logout();
            return RedirectToAction("GetMovie", "Collection");
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (ModelState.IsValid)
            {
                await _userRepo.Logout();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SignUp()
        {

            if (ModelState.IsValid)
            {
                await _userRepo.Logout();
            }
            return View();
        }
        [Authorize]
        public IActionResult Profile()
        {
            ViewBag.EmailData = _userRepo.GetUserEmail(User.Identity.Name);
            var ProfilePicturePath = _userRepo.GetUserProfilePicture(User.Identity.Name);
            var extension = Path.GetExtension(ProfilePicturePath);
            ViewBag.ProfilePicturePath = User.Identity.Name + extension;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = _recordsRepo.GetRecordsByUserId(userId);
            var tuple = new Tuple<List<Records>, UpdateUserModel>(list, new UpdateUserModel());

            return View(tuple);
        }

        [Authorize]
        public IActionResult DeleteRecord(string deleteButton)
        {
            ViewBag.EmailData = _userRepo.GetUserEmail(User.Identity.Name);
            Records del = System.Text.Json.JsonSerializer.Deserialize<Records>(deleteButton);

            _recordsRepo.Delete(del);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = _recordsRepo.GetRecordsByUserId(userId);
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

                    _recordsRepo.Add(new Records { Name = name, UserId = User.FindFirstValue(ClaimTypes.NameIdentifier), Year = year, Type = type, ModifiedBy = User.FindFirstValue(ClaimTypes.NameIdentifier) });
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

                Records r = _recordsRepo.GetById(id);
                r.Name = name; r.Year = year; r.Type = type; r.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); r.ModifiedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _recordsRepo.Update(r);
            }
            return RedirectToAction("Profile");
        }
        [Authorize]
        public IActionResult EditRecord(int id)
        {
            Records r = _recordsRepo.GetById(id);
            return View("EditRecord", r);
        }
        [Authorize]
        [HttpPost]
        public async Task<string> UpdatePassword(UpdateUserModel model)
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

        
        public async Task<IActionResult> VerifyEmail(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                return BadRequest("Invalid Email Or Token.");
            }

            var result = await _userRepo.VerifyEmail(email, token);

            if (result)
            {
                object data = "Email Confirmed";
                return View("ThankYou", data);
            }
            else
            {
                object data = "Error Confirming Your Email Address.";
                return View("ThankYou", data);

            }
        }

        [Authorize]
        public IActionResult UpdateProfilePicture(List<IFormFile> updatedProfilePicture)
        {
            if(updatedProfilePicture.Count==0)
            {
                return BadRequest("Error");
            }
            if (ModelState.IsValid)
            {
                var result = _userRepo.UpdateProfilePicture(updatedProfilePicture[0], User.Identity.Name);
                if (result)
                {
                    return Content("Profile Picture Updated Successfully");
                }
                else
                {
                    return Content("Error While Updating The Profile Picture");
                }
            }
            else
            {
                return Content("Error While Updating The Profile Picture");
            }
        }

    }
}
