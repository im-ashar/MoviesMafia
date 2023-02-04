namespace MoviesMafia.Models
{
    public class PlaynowResults
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public string Iso6391 { get; set; }
        public string Iso31661 { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string Site { get; set; }
        public int Size { get; set; }
        public string Type { get; set; }
        public bool Official { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Id { get; set; }
    }

    public class PlaynowRoot
    {
        public int Id { get; set; }
        public List<PlaynowResults> Results { get; set; }
        public bool API_Fetched { get; set; }

    }




}