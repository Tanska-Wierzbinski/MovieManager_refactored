using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IQueryable<MovieResultDto>> GetMoviesByCategory(int id);
        Task Add(CategoryAddDto category);
        Task<CategoryDto> EditGet(int id);
        Task EditPost(CategoryDto category);
        Task<CategoryDto> GetById();
        Task<bool> Remove(int id);
    }
}
