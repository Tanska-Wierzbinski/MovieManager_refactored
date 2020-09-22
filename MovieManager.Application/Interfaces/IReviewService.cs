using MovieManager.Application.DTOs.Review;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Application.Interfaces
{
    public interface IReviewService
    {
        Task AddPost(ReviewAddDto review);
        Task<ReviewDto> EditGet(int id);
        Task EditPost(ReviewDto review);
        Task<ReviewDto> GetById();
        Task<bool> Remove(int id);
    }
}
