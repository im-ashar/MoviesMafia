using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesMafia.Models;

namespace MoviesMafia.Controllers
{
    public class SearchMoviesController : Controller
    {
        public static string searchPassed { get; set; }
        [HttpPost]
        public IActionResult SearchMovies(string search)
        {
            searchPassed = search;
            SearchMovies searchMovie = new SearchMovies();
            ViewBag.x = searchPassed;
            if (searchMovie.SearchMovie(search).Result.API_Fetched)
            {

                return View(searchMovie.SearchMovie(search));
            }
            else
            {
                return View("ApiNotFetched");

            }
        }

        [Authorize]
        public IActionResult Page(int page)
        {

            ViewBag.x = searchPassed;
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

    }


}
