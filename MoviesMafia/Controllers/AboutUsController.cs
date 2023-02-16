using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MoviesMafia.Controllers
{
    public class AboutUsController : Controller
    {
        [Authorize(Roles ="Admin")]
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
