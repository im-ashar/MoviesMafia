using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesMafia.Models;

namespace MoviesMafia.Controllers
{
    public class SeasonController : Controller
    {
        public IActionResult Season()
        {
            SeasonAPI api = new SeasonAPI();
            if (api.SeasonApiCall().Result.API_Fetched)
            {
                return View(api.SeasonApiCall());
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
            SeasonAPI api = new SeasonAPI();
            if (api.SeasonApiCall().Result.API_Fetched)
            {

                return View("Season", api.Page(page));
            }
            else
            {
                return View("ApiNotFetched");
            }
        }
        [Authorize]
        public IActionResult Playnow(int playnow, string title)
        {
            PlaynowAPI api = new PlaynowAPI();
            if (api.PlaynowSeasonApiCall(playnow).Result.API_Fetched)
            {
                ViewBag.PlaynowTitle = title;
                return View("Playnow", api.PlaynowSeasonApiCall(playnow));
            }
            else
            {
                return View("ApiNotFetched");
            }

        }
    }

}
