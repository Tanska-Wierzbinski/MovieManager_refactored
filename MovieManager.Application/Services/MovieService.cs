using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieManager.Application.DTOs.Actor;
using MovieManager.Application.DTOs.Category;
using MovieManager.Application.DTOs.Home;
using MovieManager.Application.DTOs.Movie;
using MovieManager.Application.Interfaces;
using MovieManager.Domain.Interfaces;
using MovieManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMovieActorRepository _movieActorRepository;
        private readonly IMovieCategoryRepository _movieCategoryRepository;
        private readonly IMapper _mapper;

        public MovieService(IActorRepository actorRepository, IMovieRepository movieRepository, ICategoryRepository categoryRepository, IMovieActorRepository movieActorRepository, IMovieCategoryRepository movieCategoryRepository, IMapper mapper)
        {
            _actorRepository = actorRepository;
            _movieRepository = movieRepository;
            _categoryRepository = categoryRepository;
            _movieActorRepository = movieActorRepository;
            _movieCategoryRepository = movieCategoryRepository;
            _mapper = mapper;
        }
        public MovieAddDto AddGet()
        {
            return new MovieAddDto()
            {
                Actors = _actorRepository.GetAll().Select(a => new SelectListItem { Text = a.Name + " " + a.LastName, Value = a.Id.ToString() }).ToList(),
                Categories = _categoryRepository.GetAll().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToList()
            };
        }

        public async Task AddPost(MovieAddDto movie)
        {
            var newMovie = _mapper.Map<Movie>(movie);
            await _movieRepository.UploadImage(movie.ImageFile, newMovie);
            await _movieRepository.Add(newMovie);
        }

        public async Task<MovieEditDto> EditGet(int id)
        {
            return _mapper.Map<MovieEditDto>(await _movieRepository.GetById(id));
        }

        public async Task EditPost(MovieEditDto movie)
        {
            var editedMovie = _mapper.Map<Movie>(movie);
            await _movieRepository.UploadImage(movie.ImageFile, editedMovie);

            await _movieRepository.Update(editedMovie);
        }

        public MovieIndexDto GetAllForIndex(int yearMin, int yearMax, int gradeMin, int gradeMax, int[] categories, string sortOrder, int? pageNumber, int pageSize = 5)
        {
            MovieIndexDto moviesForIndex = new MovieIndexDto()
            {
                Movies = _movieRepository.GetAll().ProjectTo<MovieDto>(_mapper.ConfigurationProvider).AsEnumerable(),
                Categories = _categoryRepository.GetAll().ProjectTo<CategoryDto>(_mapper.ConfigurationProvider).ToList(),//.AsEnumerable(),
                GradeMin = gradeMin,
                GradeMax = gradeMax == 0 ? 10 : gradeMax,
                YearMax = yearMax == 0 ? 2100 : yearMax,
                YearMin = yearMin == 0 ? yearMax : yearMin,
                CategoriesIds = categories,
                SortOrder = sortOrder,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            //moviesForIndex = 
                Filter(moviesForIndex);
            //moviesForIndex.Movies = Sort(sortOrder, moviesForIndex.Movies);
            moviesForIndex.PaginatedMovies = PaginatedList<MovieDto>.Create(moviesForIndex.Movies.AsQueryable(), pageNumber ?? 1, pageSize);

            return moviesForIndex;
        }



        

        private class MovieDtoComparer : IEqualityComparer<MovieDto>
        {
            public bool Equals([AllowNull]MovieDto first, [AllowNull]MovieDto second)
            {
                if (first.Id == second.Id)
                    return true;

                return false;
            }
            public int GetHashCode([DisallowNull]MovieDto obj)
            {
                return obj.Id.GetHashCode();
            }
        }

        private void Filter(MovieIndexDto moviesForIndex)
        {
            if (moviesForIndex.CategoriesIds.Length != 0)
            {
                foreach (var category in moviesForIndex.CategoriesIds)
                {
                    //var mg = _movieRepository.GetAll().ProjectTo<MovieDto>(_mapper.ConfigurationProvider).AsEnumerable();
                    var moviesWithCategory = _movieCategoryRepository.GetMoviesByCategory(category).ProjectTo<MovieDto>(_mapper.ConfigurationProvider).AsEnumerable();
                    //var m = _movieRepository.GetForCategory(category);
                    //var pom = mg.Intersect(m, new MovieDtoComparer());//.ToList();
                    moviesForIndex.Movies = moviesForIndex.Movies.Intersect(moviesWithCategory, new MovieDtoComparer());
                }
            }
            moviesForIndex.Movies = moviesForIndex.Movies.Where(m => m.ReleaseDate.Year >= moviesForIndex.YearMin && m.ReleaseDate.Year <= moviesForIndex.YearMax)
                                                         .Where(m => m.Reviews.Any())
                                                         .Where(m => m.Reviews.Average(m => m.Grade) >= moviesForIndex.GradeMin && m.Reviews.Average(m => m.Grade) < moviesForIndex.GradeMax + 1);
            //return moviesForIndex;
        }

        private IEnumerable<MovieDto> Sort(string sortOrder, IEnumerable<MovieDto> movies)
        {
            switch (sortOrder)
            {
                case "nameDesc":
                    movies = movies.OrderByDescending(m => m.Name);
                    break;
                case "yearDesc":
                    movies = movies.OrderByDescending(m => m.ReleaseDate);
                    break;
                case "year":
                    movies = movies.OrderBy(m => m.ReleaseDate);
                    break;
                case "gradeDesc":
                    movies = movies.Where(m => m.Reviews.Any()).OrderByDescending(m => m.Reviews.Average(k => k.Grade)).Union(movies);
                    break;
                case "grade":
                    movies = movies.Where(m => m.Reviews.Any()).OrderBy(m => m.Reviews.Average(k => k.Grade)).Union(movies);
                    break;
                case "quantityGradeDesc":
                    movies = movies.Where(m => m.Reviews.Any()).OrderByDescending(m => m.Reviews.Count()).Union(movies);
                    break;
                case "quantityGrade":
                    movies = movies.Where(m => m.Reviews.Any()).OrderBy(m => m.Reviews.Count()).Union(movies);
                    break;
                default:
                    movies = movies.OrderBy(m => m.Name);
                    break;
            }
            return movies;
        }

        public async Task<MovieDto> GetById(int id)
        {
            return _mapper.Map<MovieDto>(await _movieRepository.GetById(id));
        }

        public async Task<MovieDetailsDto> GetDetails(int id)
        {
            return _mapper.Map<MovieDetailsDto>(await _movieRepository.GetById(id));
        }

        public async Task<bool> Remove(int id)
        {
            await _movieRepository.Remove(await _movieRepository.GetById(id));
            return true;
        }

        public IndexDto GetForHome()
        {
            return new IndexDto()
            {
                TopMovies = _movieRepository.GetAll().Where(m => m.Reviews.Any())
                                                     .OrderByDescending(m => m.Reviews.Average(m => m.Grade)).ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
                                                     .Take(3),
                NewMovies = _movieRepository.GetAll().OrderByDescending(m => m.ReleaseDate).ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
                                                     .Take(3)
            };
        }
        public SearchDto GetForSearch(string searchString)
        {
            return new SearchDto()
            {
                Movies = _movieRepository.GetAll().Where(m => m.Name.ToLower()
                                                  .Contains(searchString.ToLower()))
                                                  .ProjectTo<MovieDto>(_mapper.ConfigurationProvider),
                Actors = _actorRepository.GetAll().Where(m => m.Name.ToLower()
                                                  .Contains(searchString.ToLower()))
                                                  .ProjectTo<ActorDto>(_mapper.ConfigurationProvider)

            };
        }
    }
}
