using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesMafia.Models;

namespace MoviesMafia.Controllers
{
    public class MoviesController : Controller
    {
        
        public IActionResult Movies()
        {
            API api = new API();
            if (api.ApiCall().Result.API_Fetched)
            {
                return View(api.ApiCall());

            }
            else
            {
                /*return View("Temp");*/
                return View("ApiNotFetched");
            }

        }

        [Authorize]
        public IActionResult Page(int page)
        {
            API api = new API();
            if (api.ApiCall().Result.API_Fetched)
            {

                return View("Movies", api.Page(page));
            }
            else
            {
                return View("ApiNotFetched");
            }
        }

        public IActionResult Playnow(int playnow)
        {
            PlaynowAPI api= new PlaynowAPI();
            if (api.PlaynowApiCall(playnow).Result.API_Fetched)
            {
                return View("Playnow", api.PlaynowApiCall(playnow));
            }
            else
            {
                return View("ApiNotFetched");
            }
            
        }
    }
}
