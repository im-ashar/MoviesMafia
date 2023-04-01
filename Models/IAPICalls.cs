namespace MoviesMafia.Models
{
    public interface IAPICalls
    {
        Task<MovieRoot> GetMovie();
        Task<MovieRoot> GetMoviePage(int page);
        Task<MovieRoot> SearchMovie(string search, string type);
        Task<MovieRoot> SearchMoviePage(string search, int page);
        Task<PlaynowRoot> PlaynowMovie(int playnow);


        Task<SeasonRoot> GetSeason();
        Task<SeasonRoot> GetSeasonPage(int page);
        Task<SeasonRoot> SearchSeason(string search, string type);
        Task<SeasonRoot> SearchSeasonPage(string search, int page);
        Task<PlaynowRoot> PlaynowSeason(int playnow);
        Task<SeasonDetailsModel> GetSeasonDetails(int playnow);
    }
}
