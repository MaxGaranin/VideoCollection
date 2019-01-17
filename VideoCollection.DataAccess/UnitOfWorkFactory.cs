using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VideoCollection.DataAccess.EfConfiguration;
using VideoCollection.Model.DataAccess;

namespace VideoCollection.DataAccess
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly DbContextOptionsBuilder _builder;

        public UnitOfWorkFactory(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            _builder = new DbContextOptionsBuilder<MoviesDbContext>();
            _builder
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString);
        }

        public IUnitOfWork Create()
        {
            var dbContext = new MoviesDbContext(_builder.Options);
            var uow = new UnitOfWork(dbContext);
            return uow;
        }
    }
}