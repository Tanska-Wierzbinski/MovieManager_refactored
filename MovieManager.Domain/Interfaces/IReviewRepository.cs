using MovieManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieManager.Domain.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        //IQueryable<Review> GetAllByMovieId(int movieId);
    }
}
