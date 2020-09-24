using MovieManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Domain.Interfaces
{
    public interface IMovieCategoryRepository : IRepository<MovieCategory>
    {
        IQueryable<Movie> GetMoviesByCategory(int id);
    }
}
