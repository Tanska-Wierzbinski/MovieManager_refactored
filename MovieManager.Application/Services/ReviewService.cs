using MovieManager.Application.DTOs.Review;
using MovieManager.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Application.Services
{
    public class ReviewService : IReviewService
    {
        public Task AddPost(ReviewAddDto review)
        {
            throw new NotImplementedException();
        }

        public Task<ReviewDto> EditGet(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditPost(ReviewDto review)
        {
            throw new NotImplementedException();
        }

        public Task<ReviewDto> GetById()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
