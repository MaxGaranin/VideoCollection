using System.Collections.Generic;

namespace VideoCollection.Model.Entities
{
    public class Movie : Entity<int>
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public int Runtime { get; set; }
        public Genre[] Genres { get; set; }
        public string Plot { get; set; }
        public string PosterUrl { get; set; }
        
        public virtual Director Director { get; set; }
        public virtual ICollection<MovieActor> MovieActors { get; set; }
    }

    public class MovieActor
    {
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }

        public int ActorId { get; set; }
        public virtual Actor Actor { get; set; }
    }

    public class Person : Entity<int>
    {
        public string Name { get; set; }
        public string SurName { get; set; }
    }

    public class Actor : Person
    {
        public virtual ICollection<MovieActor> MovieActors { get; set; }
    }

    public class Director : Person
    {
    }

    public enum Genre
    {
        Comedy,
        Fantasy,
        Crime,
        Drama,
        Music,
        Adventure,
        History,
        Thriller,
        Animation,
        Family,
        Mystery,
        Biography,
        Action,
        FilmNoir,
        Romance,
        SciFi,
        War,
        Western,
        Horror,
        Musical,
        Sport,
    }
}