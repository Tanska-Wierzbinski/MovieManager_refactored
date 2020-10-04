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
    public class ReviewRepository : Repository<Review>, IReviewRepository 
    {
        public ReviewRepository(MovieManagerContext context) : base(context)
        {

        }

        public override async Task<Review> GetById(int id)
        {
            return await Db.Reviews.SingleOrDefaultAsync(c => c.Id == id);
        }

        //public IQueryable<Review> GetAllByMovieId(int movieId)
        //{
        //    return Db.Reviews.AsNoTracking()
        //                     .Where(r => r.MovieId == movieId);
        //}
    }
}
