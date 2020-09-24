using AutoMapper;
using AutoMapper.QueryableExtensions;
using MovieManager.Application.DTOs.Category;
using MovieManager.Application.DTOs.Movie;
using MovieManager.Application.Interfaces;
using MovieManager.Domain.Interfaces;
using MovieManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMovieCategoryRepository _movieCategoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMovieCategoryRepository movieCategoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _movieCategoryRepository = movieCategoryRepository;
            _mapper = mapper;
        }

        public async Task AddPost(CategoryAddDto category)
        {
            if (category != null)
            {
                await _categoryRepository.Add(_mapper.Map<Category>(category));
            }
        }

        public async Task<CategoryDto> EditGet(int id)
        {
            return _mapper.Map<CategoryDto>(await _categoryRepository.GetById(id));
        }

        public async Task EditPost(CategoryDto category)
        {
            await _categoryRepository.Update(_mapper.Map<Category>(category));
        }

        public async Task<CategoryDto> GetById(int id)
        {
            return _mapper.Map<CategoryDto>(await _categoryRepository.GetById(id));
        }

        public CategoryDetailsDto GetMoviesByCategory(int id)
        {
            return new CategoryDetailsDto()
            {
                Id = id,
                Name = _categoryRepository.GetById(id).Result.Name,
                Movies = _movieCategoryRepository.GetMoviesByCategory(id).ProjectTo<MovieDto>(_mapper.ConfigurationProvider).ToList()
            };
        }

        public async Task<bool> Remove(int id)
        {
            await _categoryRepository.Remove(await _categoryRepository.GetById(id));
            return true;
        }
    }
}
