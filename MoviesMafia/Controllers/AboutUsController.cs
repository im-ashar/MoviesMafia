using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MoviesMafia.Controllers
{
    public class AboutUsController : Controller
    {
        
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
