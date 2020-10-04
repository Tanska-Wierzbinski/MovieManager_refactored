using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Infrastructure.Context
{
    public class MovieManagerContext : DbContext
    {
        public MovieManagerContext(DbContextOptions<MovieManagerContext> options) : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<MovieCategory> MovieCategories { get; set; }
        public DbSet<Image> Images { get; set; }

        //public MovieManagerContext(DbContextOptions<MovieManagerContext> options)
        //    :base(options)
        //{

        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActor>().HasKey(t => new { t.MovieId, t.ActorId });

            modelBuilder.Entity<MovieActor>()
                .HasOne(m => m.Movie)
                .WithMany(ma => ma.MovieActors)
                .HasForeignKey(m => m.MovieId);

            modelBuilder.Entity<MovieActor>()
                .HasOne(a => a.Actor)
                .WithMany(am => am.MovieActors)
                .HasForeignKey(a => a.ActorId);


            modelBuilder.Entity<MovieCategory>().HasKey(t => new { t.MovieId, t.CategoryId });

            modelBuilder.Entity<MovieCategory>()
                .HasOne(m => m.Movie)
                .WithMany(mc => mc.MovieCategories)
                .HasForeignKey(m => m.MovieId);

            modelBuilder.Entity<MovieCategory>()
                .HasOne(c => c.Category)
                .WithMany(cm => cm.MovieCategories)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<Review>().HasOne(r => r.Movie).WithMany(r => r.Reviews).HasForeignKey(r => r.MovieId);
        }
    }
}
