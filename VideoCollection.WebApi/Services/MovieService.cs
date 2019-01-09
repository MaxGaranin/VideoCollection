using System.Collections.Generic;
using System.Linq;
using VideoCollection.Model.DataAccess;
using VideoCollection.Model.Entities;

namespace VideoCollection.WebApi.Services
{
    public class MovieService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public MovieService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public IList<Movie> GetMovies()
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var rep = uow.MovieRepository;
                return rep.GetAll().ToList();
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