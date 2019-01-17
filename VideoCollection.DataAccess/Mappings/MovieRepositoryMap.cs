using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VideoCollection.Model.Entities;

namespace VideoCollection.DataAccess.Mappings
{
    public class MovieRepositoryMap : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasOne(t => t.Director);

            var genresConverter = new ValueConverter<Genre[], string>(
                v => string.Join(',', v),
                s => s.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(Enum.Parse<Genre>)
                    .ToArray()
            );

            builder.Property(t => t.Genres).HasConversion(genresConverter);
        }
    }

    public class ActorRepositoryMap : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.HasKey(t => t.Id);
        }
    }

    public class MovieActorRepositoryMap : IEntityTypeConfiguration<MovieActor>
    {
        public void Configure(EntityTypeBuilder<MovieActor> builder)
        {
            builder.HasKey(t => new {t.MovieId, t.ActorId});

            builder
                .HasOne(t => t.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(m => m.MovieId);

            builder
                .HasOne(t => t.Actor)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(m => m.ActorId);
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