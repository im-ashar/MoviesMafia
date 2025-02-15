using Microsoft.AspNetCore.Identity;
using MoviesMafia.Configurations;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;

namespace MoviesMafia.Models.Repo
{

    public class UserRepo : IUserRepo
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SmtpConfig _smtpConfig;

        public UserRepo(UserManager<AppUser> userManager, SignInManager<AppUser> sManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = sManager;
            _httpContextAccessor = httpContextAccessor;
            _smtpConfig = AppSettings.SmtpConfig;
        }
        public async Task<IdentityResult> SignUpAsync(UserSignUpModel model)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", model.ProfilePicture.FileName);
            var extension = Path.GetExtension(model.ProfilePicture.FileName);
            var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProfilePictures", model.Username + extension);
            var user = new AppUser
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
                    var url = _httpContextAccessor.HttpContext?.Request.Host;
                    var verificationUrl = $"https://{url}/Account/VerifyEmail?email={Uri.EscapeDataString(model.Email)}&token={Uri.EscapeDataString(token)}";
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress("admin@moviesmafia.runasp.net");
                        mail.To.Add(model.Email);
                        mail.Subject = "Verify your email address";
                        mail.Body = $"<h4>Please click the following link to verify your email address: <a href='{verificationUrl}'>Verify Me</a></h4>";
                        mail.IsBodyHtml = true;


                        using (SmtpClient smtp = new SmtpClient(_smtpConfig.Host, _smtpConfig.Port))
                        {
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);
                            smtp.EnableSsl = true;
                            await smtp.SendMailAsync(mail);
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

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> LogInAsync(UserLogInModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, true, false);
            return result;
        }
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return;
        }
        public async Task<string?> GetUserEmailAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user?.Email;
        }
        public async Task<string?> GetUserProfilePictureAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var profilePicture = user?.ProfilePicturePath;
            if (Path.DirectorySeparatorChar == '\\')
            {
                profilePicture = profilePicture?.Replace('\\', '/');
            }
            return profilePicture;
        }
        public async Task<AppUser> GetUserAsync(ClaimsPrincipal userName)
        {
            var user = await _userManager.GetUserAsync(userName);
            return user;
        }

        public async Task<IdentityResult> UpdatePasswordAsync(AppUser user, string currentPassword, string newPassword)
        {
            var update = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return update;
        }
        public async Task<IList<AppUser>> GetAllUsersAsync()
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync("User");

            return usersInRole;
        }
        public async Task<bool> DeleteUserAsync(AppUser user)
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
        public async Task<AppUser> GetUserByIdAsync(string id)
        {
            var result = await _userManager.FindByIdAsync(id);
            return result;
        }
        public async Task<bool> UpdateEmailAsync(string id, string email)
        {
            var result = await GetUserByIdAsync(id);
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
        public async Task<bool> VerifyEmailAsync(string email, string token)
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
        public async Task<bool> UpdateProfilePictureAsync(IFormFile updatedProfilePicture, string userName)
        {

            var extension = Path.GetExtension(updatedProfilePicture.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", updatedProfilePicture.FileName);
            var path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProfilePictures");


            var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProfilePictures", userName + extension);
            var user = await _userManager.FindByNameAsync(userName);
            user.ProfilePicturePath = dbPath;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    updatedProfilePicture.CopyTo(stream);
                }



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
