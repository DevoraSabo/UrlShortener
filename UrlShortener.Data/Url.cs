using System;

namespace UrlShortener.Data
{
    public class Url
    {
        public int Id { get; set; }
        public string Hash { get; set; }
        public string OriginalUrl { get; set; }
        public int? UserId { get; set; }
        //public int Views { get; set; }

        public User User { get; set; }

    }
}
