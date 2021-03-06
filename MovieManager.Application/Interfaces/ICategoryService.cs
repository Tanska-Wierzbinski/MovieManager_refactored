﻿using MovieManager.Application.DTOs.Category;
using MovieManager.Application.DTOs.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Application.Interfaces
{
    public interface ICategoryService
    {
        IQueryable<CategoryDto> GetAllForIndex();
        CategoryDetailsDto GetMoviesByCategory(int id);
        Task AddPost(CategoryAddDto category);
        Task<CategoryDto> EditGet(int id);
        Task EditPost(CategoryDto category);
        Task<CategoryDto> GetById(int id);
        Task<bool> Remove(int id);
    }
}
