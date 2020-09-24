using Microsoft.AspNetCore.Hosting;
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
            return Db.MovieCategories.Where(mc => mc.CategoryId == id).Select(mc => mc.Movie);
        }
    }
}
