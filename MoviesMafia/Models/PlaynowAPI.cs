using Newtonsoft.Json;

namespace MoviesMafia.Models
{
    public class PlaynowAPI
    {
        public async Task<PlaynowRoot> PlaynowApiCall(int playnow)
        {
            try
            {
                var API_KEY = "api_key=730ab5da75a3cf6c71a47af0ec102ec0";
                var BASE_URL = "https://api.themoviedb.org/3";
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
    }
}
