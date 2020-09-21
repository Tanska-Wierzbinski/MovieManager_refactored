using MovieManager.Domain.Interfaces;
using MovieManager.Domain.Models;
using MovieManager.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Infrastructure.Repositories
{
    public class MovieCategoryRepository : Repository<MovieCategory>, IMovieCategoryRepository
    {
        public MovieCategoryRepository(MovieManagerContext context) : base(context)
        {

        }
    }
}
