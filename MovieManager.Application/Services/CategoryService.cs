using MovieManager.Application.DTOs.Category;
using MovieManager.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Application.Services
{
    public class CategoryService : ICategoryService
    {
        public Task Add(CategoryAddDto category)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDto> EditGet(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditPost(CategoryDto category)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDto> GetById()
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDetailsDto> GetMoviesByCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
