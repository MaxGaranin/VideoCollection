using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VideoCollection.Model.Entities;

namespace VideoCollection.Model.DataAccess
{
    public interface IRepository<T, in TKey> : IDisposable
        where T : class, IEntity<TKey>, new()
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void Remove(TKey id);
        T Get(TKey id);
        T Find(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();

        IEnumerable<T> FindAll<TOrderKey>(
            Expression<Func<T, bool>> predicate = null,
            Expression<Func<T, TOrderKey>> orderBy = null,
            int? skip = null,
            int? take = null);
    }

    public interface IMovieRepository : IRepository<Movie, int>
    {
    }

    public interface IDirectorRepository : IRepository<Director, int>
    {
    }

    public interface IActorRepository : IRepository<Actor, int>
    {
    }
}