using Microsoft.AspNetCore.Mvc;
using MoviesMafia.Models.Repo;

namespace MoviesMafia.Controllers
{
    public class LandingPageController : Controller
    {
        public IActionResult LandingPage()
        {
            return View();
        }
        
    }
}
