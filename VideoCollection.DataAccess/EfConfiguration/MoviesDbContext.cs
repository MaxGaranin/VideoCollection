﻿using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VideoCollection.DataAccess.Utils;
using VideoCollection.Model.Entities;

namespace VideoCollection.DataAccess.EfConfiguration
{
    public class MoviesDbContextFactory : IDesignTimeDbContextFactory<MoviesDbContext>
    {
        MoviesDbContext IDesignTimeDbContextFactory<MoviesDbContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MoviesDbContext>();
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=VideoCollection;Trusted_Connection=True;");

            return new MoviesDbContext(optionsBuilder.Options);
        }
    }

    public class MoviesDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieActor> MovieActor { get; set; }
        public DbSet<Director> Directors { get; set; }

        public MoviesDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CreateMappings(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void CreateMappings(ModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => ReflectionsUtils.IsAssignableToGenericType(type, typeof(IEntityTypeConfiguration<>)));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}