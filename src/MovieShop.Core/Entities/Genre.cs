using System.Collections.Generic;

namespace MovieShop.Core.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}