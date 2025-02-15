using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesMafia.Models;

namespace MoviesMafia.Controllers
{
    public class CollectionController : Controller
    {
        private readonly IAPICalls _apiCalls;
        public static string searchPassed { get; set; }
        public CollectionController(IAPICalls apiCalls)
        {
            _apiCalls = apiCalls;
        }

        public async Task<IActionResult> GetMovie()
        {
            if ((await _apiCalls.GetMovie()).API_Fetched)
            {
                return View(await _apiCalls.GetMovie());

            }
            else
            {
                /*return View("Temp");*/
                return View("ApiNotFetched");
            }

        }

        [Authorize]
        public async Task<IActionResult> GetMoviePage(int page)
        {
            if ((await _apiCalls.GetMoviePage(page)).API_Fetched)
            {

                return View("GetMovie", await _apiCalls.GetMoviePage(page));
            }
            else
            {
                return View("ApiNotFetched");
            }
        }
        [Authorize]
        public async Task<IActionResult> PlaynowMovie(int playnow)
        {
            if ((await _apiCalls.PlaynowMovie(playnow)).API_Fetched)
            {

                return View("PlaynowMovie", await _apiCalls.PlaynowMovie(playnow));
            }
            else
            {
                return View("ApiNotFetched");
            }

        }

        [HttpPost]

        public async Task<IActionResult> Search(string search, string type = "Movie")
        {
            searchPassed = search;
            if (type == "Movie")
            {

                ViewBag.x = searchPassed;
                if ((await _apiCalls.SearchMovie(search, type)).API_Fetched)
                {
                    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                        return PartialView("SearchMovies", await _apiCalls.SearchMovie(search, type));
                    }
                    else
                    {
                        return View("SearchMovies", await _apiCalls.SearchMovie(search, type));
                    }
                }
                else
                {
                    return View("ApiNotFetched");

                }
            }
            else if (type == "Season")
            {

                ViewBag.x = searchPassed;
                if ((await _apiCalls.SearchSeason(search, type)).API_Fetched)
                {

                    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                        return PartialView("SearchSeason", await _apiCalls.SearchSeason(search, type));
                    }
                    else
                    {
                        return View("SearchSeason", await _apiCalls.SearchSeason(search, type));
                    }
                }
                else
                {
                    return View("ApiNotFetched");

                }
            }
            else
            {
                return View("ApiNotFetched");
            }

        }

        [Authorize]
        public async Task<IActionResult> SearchPage(int page, string type)
        {

            ViewBag.x = searchPassed;
            if (type == "Movie")
            {

                if ((await _apiCalls.SearchMoviePage(searchPassed, page)).API_Fetched)
                {
                    return View("SearchMovies", await _apiCalls.SearchMoviePage(searchPassed, page));
                }
                else
                {
                    return View("ApiNotFetched");
                }
            }
            else if (type == "Season")
            {

                if ((await _apiCalls.SearchSeasonPage(searchPassed, page)).API_Fetched)
                {
                    return View("SearchSeason", await _apiCalls.SearchSeasonPage(searchPassed, page));
                }
                else
                {
                    return View("ApiNotFetched");
                }
            }
            else
            {
                return View("ApiNotFetched");
            }
        }
        public async Task<IActionResult> GetSeason()
        {

            if ((await _apiCalls.GetSeason()).API_Fetched)
            {
                return View(await _apiCalls.GetSeason());
            }
            else
            {
                /*return View("Temp");*/
                return View("ApiNotFetched");
            }
        }
        [Authorize]
        public async Task<IActionResult> GetSeasonPage(int page)
        {

            if ((await _apiCalls.GetSeasonPage(page)).API_Fetched)
            {

                return View("GetSeason", await _apiCalls.GetSeasonPage(page));
            }
            else
            {
                return View("ApiNotFetched");
            }
        }
        [Authorize]
        public async Task<IActionResult> PlaynowSeason(int playnow, string title)
        {

            if ((await _apiCalls.PlaynowSeason(playnow)).API_Fetched)
            {
                ViewBag.PlaynowTitle = title;
                return View("PlaynowSeason", await _apiCalls.PlaynowSeason(playnow));
            }
            else
            {
                return View("ApiNotFetched");
            }

        }

        [Authorize]
        public async Task<IActionResult> PlaySeasonList(int playnow)
        {

            var result = await _apiCalls.GetSeasonDetails(playnow);
            if (result.API_Fetched)
            {
                return View("PlaySeasonList", result);
            }
            else
            {
                return View("ApiNotFetched");
            }

        }
        [Authorize]
        public IActionResult PlaySeason(string playnow)
        {

            return View("PlaySeason", playnow);
        }
        [Authorize]
        public IActionResult PlayMovie(int playnow)
        {

            return View("PlayMovie", playnow);
        }
    }
}
