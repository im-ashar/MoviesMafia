using Newtonsoft.Json;

namespace MoviesMafia.Models
{
    public class APICalls : IAPICalls
    {
        string API_KEY = "api_key=730ab5da75a3cf6c71a47af0ec102ec0";
        string BASE_URL = "https://api.themoviedb.org/3";

        public async Task<MovieRoot> GetMovie()
        {
            try
            {
                var API_URL = BASE_URL + "/discover/movie?sort_by=popularity.desc&" + API_KEY;
                // Create a new HTTP client
                HttpClient client = new HttpClient();
                // Make the request and retrieve the response
                HttpResponseMessage response = await client.GetAsync(API_URL);

                // Read the response as a string
                var responseBody = await response.Content.ReadAsStringAsync();
                MovieRoot obj = JsonConvert.DeserializeObject<MovieRoot>(responseBody);
                obj.API_Fetched = true;
                return obj;
            }
            catch
            {
                MovieRoot obj = new MovieRoot();
                obj.API_Fetched = false;
                return obj;
            }
        }

        public async Task<MovieRoot> GetMoviePage(int page)
        {
            try
            {
                
                var API_URL = BASE_URL + "/discover/movie?sort_by=popularity.desc&" + API_KEY;


                // Create a new HTTP client
                HttpClient client = new HttpClient();

                // Make the request and retrieve the response
                HttpResponseMessage response = await client.GetAsync(API_URL + "&page=" + page);

                // Read the response as a string
                var responseBody = await response.Content.ReadAsStringAsync();
                MovieRoot obj = JsonConvert.DeserializeObject<MovieRoot>(responseBody);

                obj.API_Fetched = true;
                return obj;
            }
            catch
            {
                MovieRoot obj = new MovieRoot();
                obj.API_Fetched = false;
                return obj;
            }
        }

        public async Task<MovieRoot> SearchMovie(string search, string type)
        {
            try
            {
                
                var SEARCH_URL = BASE_URL + "/search/movie?" + API_KEY;


                // Create a new HTTP client
                HttpClient client = new HttpClient();

                // Make the request and retrieve the response
                HttpResponseMessage response = await client.GetAsync(SEARCH_URL + "&query=" + search);

                // Read the response as a string
                var responseBody = await response.Content.ReadAsStringAsync();
                MovieRoot obj = JsonConvert.DeserializeObject<MovieRoot>(responseBody);

                obj.API_Fetched = true;
                return obj;
            }
            catch
            {
                MovieRoot obj = new MovieRoot();
                obj.API_Fetched = false;
                return obj;
            }
        }

        public async Task<MovieRoot> SearchMoviePage(string search, int page)
        {
            try
            {
                
                var SEARCH_URL = BASE_URL + "/search/movie?" + API_KEY;


                // Create a new HTTP client
                HttpClient client = new HttpClient();

                // Make the request and retrieve the response
                HttpResponseMessage response = await client.GetAsync(SEARCH_URL + "&query=" + search + "&page=" + page);

                // Read the response as a string
                var responseBody = await response.Content.ReadAsStringAsync();
                MovieRoot obj = JsonConvert.DeserializeObject<MovieRoot>(responseBody);
                obj.API_Fetched = true;
                return obj;
            }
            catch
            {
                MovieRoot obj = new MovieRoot();
                obj.API_Fetched = false;
                return obj;
            }
        }

        public async Task<PlaynowRoot> PlaynowMovie(int playnow)
        {
            try
            {
                
                var API_URL = BASE_URL + "/movie/" + playnow + "/videos?" + API_KEY;


                // Create a new HTTP client
                HttpClient client = new HttpClient();

                // Make the request and retrieve the response
                HttpResponseMessage response = await client.GetAsync(API_URL);

                // Read the response as a string
                var responseBody = await response.Content.ReadAsStringAsync();
                PlaynowRoot obj = JsonConvert.DeserializeObject<PlaynowRoot>(responseBody);
                obj.API_Fetched = true;
                return obj;
            }
            catch
            {
                PlaynowRoot obj = new PlaynowRoot();
                obj.API_Fetched = false;
                return obj;
            }
        }


        public async Task<SeasonRoot> GetSeason()
        {
            try
            {
               
                var API_URL = BASE_URL + "/discover/tv?sort_by=popularity.desc&" + API_KEY;


                // Create a new HTTP client
                HttpClient client = new HttpClient();

                // Make the request and retrieve the response
                HttpResponseMessage response = await client.GetAsync(API_URL);

                // Read the response as a string
                var responseBody = await response.Content.ReadAsStringAsync();
                SeasonRoot obj = JsonConvert.DeserializeObject<SeasonRoot>(responseBody);
                obj.API_Fetched = true;
                return obj;
            }
            catch
            {
                SeasonRoot obj = new SeasonRoot();
                obj.API_Fetched = false;
                return obj;
            }
        }

        public async Task<SeasonRoot> GetSeasonPage(int page)
        {
            try
            {
                
                var API_URL = BASE_URL + "/discover/tv?sort_by=popularity.desc&" + API_KEY;


                // Create a new HTTP client
                HttpClient client = new HttpClient();

                // Make the request and retrieve the response
                HttpResponseMessage response = await client.GetAsync(API_URL + "&page=" + page);

                // Read the response as a string
                var responseBody = await response.Content.ReadAsStringAsync();
                SeasonRoot obj = JsonConvert.DeserializeObject<SeasonRoot>(responseBody);

                obj.API_Fetched = true;
                return obj;
            }
            catch
            {
                SeasonRoot obj = new SeasonRoot();
                obj.API_Fetched = false;
                return obj;
            }
        }

        public async Task<SeasonRoot> SearchSeason(string search, string type)
        {
            try
            {
                
                var SEARCH_URL = BASE_URL + "/search/tv?" + API_KEY;


                // Create a new HTTP client
                HttpClient client = new HttpClient();

                // Make the request and retrieve the response
                HttpResponseMessage response = await client.GetAsync(SEARCH_URL + "&query=" + search);

                // Read the response as a string
                var responseBody = await response.Content.ReadAsStringAsync();
                SeasonRoot obj = JsonConvert.DeserializeObject<SeasonRoot>(responseBody);

                obj.API_Fetched = true;
                return obj;
            }
            catch
            {
                SeasonRoot obj = new SeasonRoot();
                obj.API_Fetched = false;
                return obj;
            }
        }

        public async Task<SeasonRoot> SearchSeasonPage(string search, int page)
        {
            try
            {
                
                var SEARCH_URL = BASE_URL + "/search/tv?" + API_KEY;


                // Create a new HTTP client
                HttpClient client = new HttpClient();

                // Make the request and retrieve the response
                HttpResponseMessage response = await client.GetAsync(SEARCH_URL + "&query=" + search + "&page=" + page);

                // Read the response as a string
                var responseBody = await response.Content.ReadAsStringAsync();
                SeasonRoot obj = JsonConvert.DeserializeObject<SeasonRoot>(responseBody);
                obj.API_Fetched = true;
                return obj;
            }
            catch
            {
                SeasonRoot obj = new SeasonRoot();
                obj.API_Fetched = false;
                return obj;
            }
        }

        public async Task<PlaynowRoot> PlaynowSeason(int playnow)
        {
            try
            {
                
                var API_URL = BASE_URL + "/tv/" + playnow + "/videos?" + API_KEY;


                // Create a new HTTP client
                HttpClient client = new HttpClient();

                // Make the request and retrieve the response
                HttpResponseMessage response = await client.GetAsync(API_URL);

                // Read the response as a string
                var responseBody = await response.Content.ReadAsStringAsync();
                PlaynowRoot obj = JsonConvert.DeserializeObject<PlaynowRoot>(responseBody);
                obj.API_Fetched = true;
                return obj;
            }
            catch
            {
                PlaynowRoot obj = new PlaynowRoot();
                obj.API_Fetched = false;
                return obj;
            }
        }
        public async Task<SeasonDetailsModel> GetSeasonDetails(int playnow)
        {
            try
            {
                
                var API_URL = BASE_URL + $"/tv/{playnow}?" + API_KEY;


                // Create a new HTTP client
                HttpClient client = new HttpClient();

                // Make the request and retrieve the response
                HttpResponseMessage response = await client.GetAsync(API_URL);

                // Read the response as a string
                var responseBody = await response.Content.ReadAsStringAsync();
                SeasonDetailsModel obj = JsonConvert.DeserializeObject<SeasonDetailsModel>(responseBody);

                obj.API_Fetched = true;
                return obj;
            }
            catch
            {
                SeasonDetailsModel obj = new SeasonDetailsModel();
                obj.API_Fetched = false;
                return obj;
            }
        }
    }
}
