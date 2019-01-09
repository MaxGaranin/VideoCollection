using System;

namespace VideoCollection.Model.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IMovieRepository MovieRepository { get; }
        IActorRepository ActorRepository { get; }
        IDirectorRepository DirectorRepository { get; }
    }
}