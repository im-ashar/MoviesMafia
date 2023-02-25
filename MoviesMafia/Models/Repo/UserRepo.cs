using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Net.Mail;
using System.Net;
using MoviesMafia.Migrations;

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
            var extension = Path.GetExtension(model.ProfilePicture.FileName);
            var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProfilePictures", model.Username + extension);
            var user = new ExtendedIdentityUser
            {
                UserName = model.Username,
                Email = model.Email,
                EmailConfirmed = false,
                LockoutEnabled = false,
                ProfilePicturePath = dbPath
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
                if (result.Succeeded)
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        model.ProfilePicture.CopyTo(stream);

                    }
                    File.Move(path, dbPath, true);
                    await _userManager.AddToRoleAsync(user, "User");
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var verificationUrl = $"http://localhost:5248/Account/VerifyEmail?email={Uri.EscapeDataString(model.Email)}&token={Uri.EscapeDataString(token)}";
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress("admin@moviesmafia.ga");
                        mail.To.Add(model.Email);
                        mail.Subject = "Verify your email address";
                        mail.Body = $"<h4>Please click the following link to verify your email address: <a href='{verificationUrl}'>Verify Me</a></h4>";
                        mail.IsBodyHtml = true;


                        using (SmtpClient smtp = new SmtpClient("live.smtp.mailtrap.io", 587))
                        {
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new NetworkCredential("api", "15ae06ca69c50761268d7bd63c110c20");
                            smtp.EnableSsl = true;
                            smtp.Send(mail);
                        }
                    }
                    return result;
                }
                else
                {
                    return result;
                }

            }
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> LogIn(UserLogInModel model)
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
            if (Path.DirectorySeparatorChar == '\\')
            {
                profilePicture = profilePicture.Replace('\\', '/');
            }
            return profilePicture;
        }
        public async Task<ExtendedIdentityUser> GetUser(ClaimsPrincipal userName)
        {
            var user = await _userManager.GetUserAsync(userName);
            return user;
        }

        public async Task<IdentityResult> UpdatePassword(ExtendedIdentityUser user, string currentPassword, string newPassword)
        {
            var update = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return update;
        }
        public async Task<IList<ExtendedIdentityUser>> GetAllUsers()
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync("User");

            return usersInRole;
        }
        public async Task<bool> DeleteUser(ExtendedIdentityUser user)
        {
            var result = await _userManager.DeleteAsync(user);



            if (result.Succeeded)
            {
                var path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProfilePictures");
                DirectoryInfo dir = new DirectoryInfo(path2);
                FileInfo[] files = dir.GetFiles(user.UserName + ".*");
                if (files.Length > 0)
                {
                    foreach (var file in files)
                    {
                        file.Delete();
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<ExtendedIdentityUser> GetUserById(string id)
        {
            var result = await _userManager.FindByIdAsync(id);
            return result;
        }
        public async Task<bool> UpdateEmail(string id, string email)
        {
            var result = await GetUserById(id);
            result.Email = email;
            var update = await _userManager.UpdateAsync(result);
            if (update.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> VerifyEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);

                if (result.Succeeded)
                {
                    return true;
                }
            }

            return false;
        }
        public bool UpdateProfilePicture(IFormFile updatedProfilePicture, string userName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", updatedProfilePicture.FileName);
            var extension = Path.GetExtension(updatedProfilePicture.FileName);
            var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProfilePictures", userName + extension);
            var user = _userManager.FindByNameAsync(userName);
            user.Result.ProfilePicturePath = dbPath;
            var result = _userManager.UpdateAsync(user.Result);
            if (result.Result.Succeeded)
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    updatedProfilePicture.CopyTo(stream);

                }

                var path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProfilePictures");
                DirectoryInfo dir = new DirectoryInfo(path2);
                FileInfo[] files = dir.GetFiles(userName + ".*");
                if (files.Length > 0)
                {
                    foreach (var file in files)
                    {
                        file.Delete();
                    }
                }
                File.Move(path, dbPath, true);
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}
