using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using VideoCollection.Model.DataAccess;
using VideoCollection.Model.Entities;

namespace VideoCollection.DataAccess.Repositories
{
    public class EfBaseRepository<T, TKey> : IRepository<T, TKey>
        where T : class, IEntity<TKey>, new()
    {
        protected DbContext DbContext { get; private set; }
        protected DbSet<T> DbSet { get; private set; }

        public EfBaseRepository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
            DbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            EntityEntry<T> dbEntityEntry = DbContext.Entry(entity);

            if (dbEntityEntry.State == EntityState.Detached)
            {
                T entity1 = DbSet.Find(entity.Id);
                DbContext.Entry(entity1).CurrentValues.SetValues(entity);
            }
            else
            {
                dbEntityEntry.State = EntityState.Modified;
            }

            DbContext.SaveChanges();
        }

        public void Remove(T entity)
        {
            this.DbSet.Attach(entity);
            this.DbSet.Remove(entity);

            DbContext.SaveChanges();
        }

        public void Remove(TKey id)
        {
            var entity = new T {Id = id};
            DbSet.Attach(entity);
            DbSet.Remove(entity);

            DbContext.SaveChanges();
        }

        public T Get(TKey id)
        {
            return DbSet.Find(id);
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.AsEnumerable();
        }

        public IEnumerable<T> FindAll<TOrderKey>(
            Expression<Func<T, bool>> predicate = null,
            Expression<Func<T, TOrderKey>> orderBy = null,
            int? skip = null,
            int? take = null)
        {
            var query = DbSet.AsQueryable();
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) query = query.OrderBy(orderBy);
            if (skip != null) query = query.Skip(skip.Value);
            if (take != null) query = query.Take(take.Value);

            return query;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || this.DbContext == null) return;

            this.DbContext.Dispose();
            this.DbSet = null;
        }
    }
}