namespace MoviesMafia.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class SeasonDetailsModel
    {
        public bool? Adult { get; set; }
        public string? Backdrop_Path { get; set; }
        public List<CreatedBy>? Created_By { get; set; }
        public List<int>? Episode_Run_Time { get; set; }
        public string? First_Air_Date { get; set; }
        public List<Genre>? Genres { get; set; }
        public string? Homepage { get; set; }
        public int? Id { get; set; }
        public bool? In_Production { get; set; }
        public List<string>? Languages { get; set; }
        public string? Last_Air_Date { get; set; }
        public LastEpisodeToAir Last_Episode_To_Air { get; set; }
        public string? Name { get; set; }
        public object Next_Episode_To_Air { get; set; }
        public List<Network>? Networks { get; set; }
        public int? Number_Of_Episodes { get; set; }
        public int? Number_Of_Seasons { get; set; }
        public List<string>? Origin_Country { get; set; }
        public string? Original_Language { get; set; }
        public string? Original_Name { get; set; }
        public string? Overview { get; set; }
        public double? Popularity { get; set; }
        public string? Poster_Path { get; set; }
        public List<ProductionCompany>? Production_Companies { get; set; }
        public List<ProductionCountry>? Production_Countries { get; set; }
        public List<Season>? Seasons { get; set; }
        public List<SpokenLanguage>? Spoken_Languages { get; set; }
        public string? Status { get; set; }
        public string? Tagline { get; set; }
        public string? Type { get; set; }
        public double? Vote_Average { get; set; }
        public int? Vote_Count { get; set; }
        public bool API_Fetched { get; set; } = false;
    }
    public class CreatedBy
    {
        public int? Id { get; set; }
        public string? Credit_Id { get; set; }
        public string? Name { get; set; }
        public int? Gender { get; set; }
        public string? Profile_Path { get; set; }
    }

    public class Genre
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }

    public class LastEpisodeToAir
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Overview { get; set; }
        public double? Vote_Average { get; set; }
        public int? Vote_Count { get; set; }
        public string? Air_Date { get; set; }
        public int? Episode_Number { get; set; }
        public string? Production_Code { get; set; }
        public int? Runtime { get; set; }
        public int? Season_Number { get; set; }
        public int? Show_Id { get; set; }
        public string? Still_Path { get; set; }
    }

    public class Network
    {
        public int? Id { get; set; }
        public string? Logo_Path { get; set; }
        public string? Name { get; set; }
        public string? Origin_Country { get; set; }
    }

    public class ProductionCompany
    {
        public int? Id { get; set; }
        public string? Logo_Path { get; set; }
        public string? Name { get; set; }
        public string? Origin_Country { get; set; }
    }

    public class ProductionCountry
    {
        public string? Iso_3166_1 { get; set; }
        public string? Name { get; set; }
    }



    public class Season
    {
        public string? Air_Date { get; set; }
        public int? Episode_Count { get; set; }
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Overview { get; set; }
        public string? Poster_Path { get; set; }
        public int? Season_Number { get; set; }
    }

    public class SpokenLanguage
    {
        public string? English_Name { get; set; }
        public string? Iso_639_1 { get; set; }
        public string? Name { get; set; }
    }
}

