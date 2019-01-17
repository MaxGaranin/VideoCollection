using System.Collections.Generic;

namespace VideoCollection.WebApi.ViewModels
{
    public class MovieContainer
    {
        public int MoviesCount { get; set; }
        public IList<MovieVM> Movies { get; set; }
    }

    public class MovieVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int Runtime { get; set; }
        public string[] Genres { get; set; }
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string PosterUrl { get; set; }
    }
}