using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoCollection.Model.Entities;

namespace VideoCollection.DataAccess.Mappings
{
    public class MovieRepositoryMap : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasMany(t => t.Actors);
            builder.HasOne(t => t.Director);
        }
    }

    public class ActorRepositoryMap : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.HasKey(t => t.Id);
        }
    }

    public class DirectorRepositoryMap : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> builder)
        {
            builder.HasKey(t => t.Id);
        }
    }
}