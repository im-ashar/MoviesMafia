using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesMafia.Models;

namespace MoviesMafia.Controllers
{
    public class SearchController : Controller
    {
        public static string searchPassed { get; set; }
        [HttpPost]
        public IActionResult Search(string search, string type)
        {
            searchPassed = search;
            if (type == "Movie")
            {
                SearchMovies searchMovie = new SearchMovies();
                ViewBag.x = searchPassed;
                if (searchMovie.SearchMovie(search, type).Result.API_Fetched)
                {
                    return View("SearchMovies", searchMovie.SearchMovie(search, type));
                }
                else
                {
                    return View("ApiNotFetched");

                }
            }
            else if (type == "Season")
            {
                SearchSeasons searchSeason = new SearchSeasons();
                ViewBag.x = searchPassed;
                if (searchSeason.SearchSeason(search, type).Result.API_Fetched)
                {

                    return View("SearchSeason", searchSeason.SearchSeason(search, type));
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
        public IActionResult Page(int page, string type)
        {

            ViewBag.x = searchPassed;
            if (type == "Movie")
            {
                SearchMovies searchMovies = new SearchMovies();
                if (searchMovies.SearchMoviePage(searchPassed, page).Result.API_Fetched)
                {
                    return View("SearchMovies", searchMovies.SearchMoviePage(searchPassed, page));
                }
                else
                {
                    return View("ApiNotFetched");
                }
            }
            else if (type == "Season")
            {
                SearchSeasons searchSeasons = new SearchSeasons();
                if (searchSeasons.SearchSeasonPage(searchPassed, page).Result.API_Fetched)
                {
                    return View("SearchSeason", searchSeasons.SearchSeasonPage(searchPassed, page));
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

    }


}
