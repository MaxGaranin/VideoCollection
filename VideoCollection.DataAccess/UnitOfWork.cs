using VideoCollection.DataAccess.EfConfiguration;
using VideoCollection.DataAccess.Repositories;
using VideoCollection.Infrastructure;
using VideoCollection.Infrastructure.Repositories;

namespace VideoCollection.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MoviesDbContext _dbContext;

        private IMovieRepository _movieRepository;
        private IActorRepository _actorRepository;
        private IDirectorRepository _directorRepository;

        public UnitOfWork(MoviesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IMovieRepository MovieRepository => _movieRepository ?? (_movieRepository = new MovieRepository(_dbContext));
        public IActorRepository ActorRepository => _actorRepository ?? (_actorRepository = new ActorRepository(_dbContext));
        public IDirectorRepository DirectorRepository => _directorRepository ?? (_directorRepository = new DirectorRepository(_dbContext));

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}