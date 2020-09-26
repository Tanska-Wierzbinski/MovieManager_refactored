using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MovieManager.Domain.Interfaces;
using MovieManager.Domain.Models;
using MovieManager.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Infrastructure.Repositories
{
    public class MovieCategoryRepository : Repository<MovieCategory>, IMovieCategoryRepository
    {
        public MovieCategoryRepository(MovieManagerContext context) : base(context)
        {

        }

        public IQueryable<Movie> GetMoviesByCategory(int id)
        {
            //return Db.MovieCategories.Include(m => m.Movie)
            //                         .ThenInclude(r => r.Reviews)
            //                         .Include(m => m.Category)
            //                         .Where(mc => mc.CategoryId == id)
            //                         .Select(mc => mc.Movie);

            var Categories = Db.Categories.Include(c => c.MovieCategories).ThenInclude(m => m.Movie).ThenInclude(r => r.Reviews);
            Category Category = Categories.Where(c => c.Id == id).Single();
            var Movies = Category.MovieCategories.Select(m => m.Movie).AsQueryable();

            return Movies;
        }
    }
}
