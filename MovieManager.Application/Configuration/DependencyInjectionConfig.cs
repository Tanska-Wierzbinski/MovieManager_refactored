using Microsoft.Extensions.DependencyInjection;
using MovieManager.Application.Interfaces;
using MovieManager.Application.Services;
using MovieManager.Domain.Interfaces;
using MovieManager.Infrastructure.Context;
using MovieManager.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Application.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MovieManagerContext>();

            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IMovieActorRepository, MovieActorRepository>();
            services.AddScoped<IMovieCategoryRepository, MovieCategoryRepository>();

            services.AddScoped<IActorService, ActorService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IReviewService, ReviewService>();

            return services;
        }
    }
}
