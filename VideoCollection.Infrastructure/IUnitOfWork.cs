using System;
using VideoCollection.Infrastructure.Repositories;

namespace VideoCollection.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IMovieRepository MovieRepository { get; }
        IActorRepository ActorRepository { get; }
        IDirectorRepository DirectorRepository { get; }
    }
}