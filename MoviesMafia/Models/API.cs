using Microsoft.JSInterop.Implementation;
using Newtonsoft.Json;

namespace MoviesMafia.Models
{
    public class API
    {
        public async Task<Root> ApiCall()
        {
            try
            {
                var API_KEY = "api_key=730ab5da75a3cf6c71a47af0ec102ec0";
                var BASE_URL = "https://api.themoviedb.org/3";
                var API_URL = BASE_URL + "/discover/movie?sort_by=popularity.desc&" + API_KEY;


                // Create a new HTTP client
                HttpClient client = new HttpClient();

                // Make the request and retrieve the response
                HttpResponseMessage response = await client.GetAsync(API_URL);

                // Read the response as a string
                var responseBody = await response.Content.ReadAsStringAsync();
                Root obj = JsonConvert.DeserializeObject<Root>(responseBody);
                obj.API_Fetched = true;
                return obj;
            }
            catch
            {
                Root obj = new Root();
                obj.API_Fetched = false;
                return obj;
            }

        }
        public async Task<Root> Page(int page)
        {
            try
            {
                var API_KEY = "api_key=730ab5da75a3cf6c71a47af0ec102ec0";
                var BASE_URL = "https://api.themoviedb.org/3";
                var API_URL = BASE_URL + "/discover/movie?sort_by=popularity.desc&" + API_KEY;


                // Create a new HTTP client
                HttpClient client = new HttpClient();

                // Make the request and retrieve the response
                HttpResponseMessage response = await client.GetAsync(API_URL + "&page=" + page);

                // Read the response as a string
                var responseBody = await response.Content.ReadAsStringAsync();
                Root obj = JsonConvert.DeserializeObject<Root>(responseBody);

                obj.API_Fetched = true;
                return obj;
            }
            catch
            {
                Root obj = new Root();
                obj.API_Fetched = false;
                return obj;
            }
        }
    }
}
