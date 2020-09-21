using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Application.Interfaces
{
    public interface IReviewService
    {
        Task AddPost(ReviewAddDto grade);
        Task<ReviewDto> EditGet(int id);
        Task EditPost(ReviewDto actor);
        Task<ReviewDto> GetById();
        Task<bool> Remove(int id);
    }
}
