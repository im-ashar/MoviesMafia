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

        public IActionResult GetMovie()
        {
            if (_apiCalls.GetMovie().Result.API_Fetched)
            {
                return View(_apiCalls.GetMovie());

            }
            else
            {
                /*return View("Temp");*/
                return View("ApiNotFetched");
            }

        }

        [Authorize]
        public IActionResult GetMoviePage(int page)
        {
            if (_apiCalls.GetMoviePage(page).Result.API_Fetched)
            {

                return View("GetMovie", _apiCalls.GetMoviePage(page));
            }
            else
            {
                return View("ApiNotFetched");
            }
        }
        [Authorize]
        public IActionResult PlaynowMovie(int playnow)
        {
            if (_apiCalls.PlaynowMovie(playnow).Result.API_Fetched)
            {

                return View("PlaynowMovie", _apiCalls.PlaynowMovie(playnow));
            }
            else
            {
                return View("ApiNotFetched");
            }

        }

        [HttpPost]

        public IActionResult Search(string search, string type = "Movie")
        {
            searchPassed = search;
            if (type == "Movie")
            {

                ViewBag.x = searchPassed;
                if (_apiCalls.SearchMovie(search, type).Result.API_Fetched)
                {
                    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                        return PartialView("SearchMovies", _apiCalls.SearchMovie(search, type));
                    }
                    else
                    {
                        return View("SearchMovies", _apiCalls.SearchMovie(search, type));
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
                if (_apiCalls.SearchSeason(search, type).Result.API_Fetched)
                {

                    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                        return PartialView("SearchSeason", _apiCalls.SearchSeason(search, type));
                    }
                    else
                    {
                        return View("SearchSeason", _apiCalls.SearchSeason(search, type));
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
        public IActionResult SearchPage(int page, string type)
        {

            ViewBag.x = searchPassed;
            if (type == "Movie")
            {

                if (_apiCalls.SearchMoviePage(searchPassed, page).Result.API_Fetched)
                {
                    return View("SearchMovies", _apiCalls.SearchMoviePage(searchPassed, page));
                }
                else
                {
                    return View("ApiNotFetched");
                }
            }
            else if (type == "Season")
            {

                if (_apiCalls.SearchSeasonPage(searchPassed, page).Result.API_Fetched)
                {
                    return View("SearchSeason", _apiCalls.SearchSeasonPage(searchPassed, page));
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
        public IActionResult GetSeason()
        {

            if (_apiCalls.GetSeason().Result.API_Fetched)
            {
                return View(_apiCalls.GetSeason());
            }
            else
            {
                /*return View("Temp");*/
                return View("ApiNotFetched");
            }
        }
        [Authorize]
        public IActionResult GetSeasonPage(int page)
        {

            if (_apiCalls.GetSeasonPage(page).Result.API_Fetched)
            {

                return View("GetSeason", _apiCalls.GetSeasonPage(page));
            }
            else
            {
                return View("ApiNotFetched");
            }
        }
        [Authorize]
        public IActionResult PlaynowSeason(int playnow, string title)
        {

            if (_apiCalls.PlaynowSeason(playnow).Result.API_Fetched)
            {
                ViewBag.PlaynowTitle = title;
                return View("PlaynowSeason", _apiCalls.PlaynowSeason(playnow));
            }
            else
            {
                return View("ApiNotFetched");
            }

        }

        [Authorize]
        public async Task<IActionResult> PlaySeasonList(int playnow)
        {
            
            var result = _apiCalls.GetSeasonDetails(playnow);
            if (result.Result.API_Fetched)
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
