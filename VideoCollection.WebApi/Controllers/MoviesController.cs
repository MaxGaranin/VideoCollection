using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VideoCollection.Model.Entities;
using VideoCollection.WebApi.Services;
using VideoCollection.WebApi.ViewModels;

namespace VideoCollection.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private readonly MovieService _movieService;

        public MoviesController(MovieService movieService)
        {
            _movieService = movieService;
        }

        // GET api/movies
        [HttpGet]
        public MovieContainer Index(int page, string sortBy, string searchStr)
        {
            var movies = _movieService.GetMovies(page, sortBy, searchStr);

            return new MovieContainer
            {
                MoviesCount = movies.Count, 
                Movies = movies
            };
        }

        // GET api/movies/5
        [HttpGet("{id}")]
        public Movie Get(int id)
        {
            return _movieService.GetMovie(id);
        }

        // POST api/movies
        [HttpPost]
        public void Post([FromBody] Movie movie)
        {
            _movieService.AddMovie(movie);
        }

        // PUT api/movies/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Movie movie)
        {
            movie.Id = id;
            _movieService.UpdateMovie(movie);
        }

        // DELETE api/movies/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _movieService.RemoveMovie(id);
        }
    }
}