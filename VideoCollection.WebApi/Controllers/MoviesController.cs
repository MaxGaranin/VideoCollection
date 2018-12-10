using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VideoCollection.Model.Entities;
using VideoCollection.WebApi.Services;

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
        public IEnumerable<Movie> Index()
        {
            return _movieService.GetMovies();
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
            _movieService.UpdateMovie(movie);
        }

        // PUT api/movies/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Movie movie)
        {
            _movieService.AddMovie(movie);
        }

        // DELETE api/movies/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _movieService.RemoveMovie(id);
        }
    }
}