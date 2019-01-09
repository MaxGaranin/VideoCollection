using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VideoCollection.DataAccess.EfConfiguration;
using VideoCollection.Infrastructure;

namespace VideoCollection.DataAccess
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IConfiguration _configuration;

        public UnitOfWorkFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IUnitOfWork Create()
        {
//            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=VideoCollection;Trusted_Connection=True;";

            var builder = new DbContextOptionsBuilder<MoviesDbContext>();
            builder.UseSqlServer(connectionString);

            var dbContext = new MoviesDbContext(builder.Options);
            var uow = new UnitOfWork(dbContext);
            return uow;
        }
    }
}