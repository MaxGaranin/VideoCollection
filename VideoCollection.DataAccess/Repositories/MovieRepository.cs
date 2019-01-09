using Microsoft.EntityFrameworkCore;
using VideoCollection.Model.DataAccess;
using VideoCollection.Model.Entities;

namespace VideoCollection.DataAccess.Repositories
{
    public class MovieRepository : EfBaseRepository<Movie, int>, IMovieRepository
    {
        public MovieRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }

    public class ActorRepository : EfBaseRepository<Actor, int>, IActorRepository
    {
        public ActorRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }

    public class DirectorRepository : EfBaseRepository<Director, int>, IDirectorRepository
    {
        public DirectorRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}