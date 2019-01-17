using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;
using VideoCollection.DataAccess.EfConfiguration;
using VideoCollection.Model.Entities;

namespace VideoCollection.DataAccess.Tests
{
    public class DbDto
    {
        public string[] Genres { get; set; }
        public MovieDto[] Movies { get; set; }
    }

    public class MovieDto
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

    public class AddDbDataTests
    {
        private MoviesDbContext _dbContext;

        [SetUp]
        public void SetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MoviesDbContext>();
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=VideoCollection;Trusted_Connection=True;");

            _dbContext = new MoviesDbContext(optionsBuilder.Options);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        [Test]
        public void AddDbDataTest()
        {
            var dbDto = JsonConvert.DeserializeObject<DbDto>(File.ReadAllText(@".\..\..\..\db.json"));

            var directorsDict = new Dictionary<string, Director>();
            var actorsDict = new Dictionary<string, Actor>();
            var movieActors = new List<MovieActor>();

            foreach (var movieDto in dbDto.Movies)
            {
                var movie = new Movie
                {
                    Title = movieDto.Title,
                    Year = movieDto.Year,
                    Runtime = movieDto.Runtime,
                    Genres = movieDto.Genres
                        .Select(x => x.Replace("-", ""))
                        .Select(Enum.Parse<Genre>)
                        .ToArray(),
                    Plot = movieDto.Plot,
                    PosterUrl = movieDto.PosterUrl
                };

                if (!directorsDict.TryGetValue(movieDto.Director, out var director))
                {
                    director = new Director {Name = movieDto.Director};
                    _dbContext.Directors.Add(director);

                    directorsDict.Add(movieDto.Director, director);
                }

                movie.Director = director;

                var actors = new List<Actor>();
                var actorDtos = movieDto.Actors.Split(", ");
                foreach (var actorDto in actorDtos)
                {
                    if (!actorsDict.TryGetValue(actorDto, out var actor))
                    {
                        actor = new Actor {Name = actorDto};
                        _dbContext.Actors.Add(actor);

                        actorsDict.Add(actorDto, actor);
                        actors.Add(actor);
                    }
                }

                movieActors.AddRange(actors.Select(x => new MovieActor
                {
                    Movie = movie,
                    Actor = x,
                }));

                _dbContext.Movies.Add(movie);
            }

            _dbContext.MovieActor.AddRange(movieActors);
            _dbContext.SaveChanges();
        }

        [Test]
        public void ClearDbDataTest()
        {
            _dbContext.Database.ExecuteSqlCommand("DELETE FROM Movies");
        }
    }
}