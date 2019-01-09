using Microsoft.Extensions.DependencyInjection;
using VideoCollection.DataAccess;
using VideoCollection.Model.DataAccess;

namespace VideoCollection.Infrastructure
{
    public class UnitOfWorkInitializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();
        }
    }
}