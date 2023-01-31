using Microsoft.AspNetCore.Mvc;

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
