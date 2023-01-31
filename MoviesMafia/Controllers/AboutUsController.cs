using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MoviesMafia.Controllers
{
    public class AboutUsController : Controller
    {
        [Authorize]
        public IActionResult AboutUs()
        {
            return View();
        }
        [Authorize]
        public IActionResult RequestMovie(string requestedMovie)
        {
            object data;
            string cookieName = "movie_name_" + HttpContext.User.Identity.Name;
            if (HttpContext.Request.Cookies.ContainsKey(cookieName))
            {
                data = "You Already Have Requested A Movie Try Again After 24 Hours";
            }
            else
            {
                data = "Your Requested Movie " + requestedMovie + " Has Been Received";
                HttpContext.Response.Cookies.Append(cookieName, requestedMovie, new CookieOptions { Expires = DateTime.Now.AddDays(1) });
            }
            return View("ThankYou", data);
        }
    }
}
