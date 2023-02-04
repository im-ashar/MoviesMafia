namespace MoviesMafia.Models
{
    public class MovieResults
    {
        public bool Adult { get; set; }
        public string Backdrop_Path { get; set; }
        public List<int> Genre_Ids { get; set; }
        public int Id { get; set; }
        public string Original_Language { get; set; }
        public string Original_Title { get; set; }
        public string Overview { get; set; }
        public double Popularity { get; set; }
        public string Poster_Path { get; set; }
        public string Release_Date { get; set; }
        public string Title { get; set; }
        public bool Video { get; set; }
        public double Vote_Average { get; set; }
        public int Vote_Count { get; set; }
    }
    public class MovieRoot
    {
        public int Page { get; set; }
        public List<MovieResults> Results { get; set; }
        public int Total_Pages { get; set; }
        public int Total_Results { get; set; }
        public bool API_Fetched { get; set; }
    }
}

