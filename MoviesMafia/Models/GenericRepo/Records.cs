﻿namespace MoviesMafia.Models.GenericRepo
{
    public class Records
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ModifiedBy { get; set; }
    }

}
