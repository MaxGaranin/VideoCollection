using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VideoCollection.Model.DataAccess;
using VideoCollection.Model.Entities;
using VideoCollection.WebApi.ViewModels;

namespace VideoCollection.WebApi.Services
{
    public class MovieService
    {
        private const int PageSize = 20;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public MovieService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public IList<MovieVM> GetMovies(int page, string sortBy, string searchStr)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var rep = uow.MovieRepository;

                Expression<Func<Movie, bool>> predicate = null;
                Expression<Func<Movie, int>> orderBy = null;
                var skip = page * PageSize;
                var take = PageSize;

                var movies = rep.FindAll(predicate, orderBy, skip, take);

                var movieVms = movies.Select(m =>
                    new MovieVM
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Year = m.Year,
                        Runtime = m.Runtime,
                        Genres = m.Genres.Select(g => g.ToString()).ToArray(),
                        Director = m.Director.Name,
                        Actors = string.Join(", ", m.MovieActors.Select(a => a.Actor.Name)),
                        Plot = m.Plot,
                        PosterUrl = m.PosterUrl
                    }).ToList();

                return movieVms;
            }
        }

        public Movie GetMovie(int id)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var rep = uow.MovieRepository;
                return rep.Get(id);
            }
        }

        public void AddMovie(Movie movie)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var rep = uow.MovieRepository;
                rep.Add(movie);
            }
        }

        public void UpdateMovie(Movie movie)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var rep = uow.MovieRepository;
                rep.Update(movie);
            }
        }

        public void RemoveMovie(int id)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var rep = uow.MovieRepository;
                rep.Remove(id);
            }
        }
    }
}