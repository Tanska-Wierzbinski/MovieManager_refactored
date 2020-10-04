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
                Actors = _actorRepository.GetAll().Select(a => new SelectListItem { Text = a.FullName, Value = a.Id.ToString() }).ToList(),
                Categories = _categoryRepository.GetAll().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToList()
            };
        }

        public async Task AddPost(MovieAddDto movie)
        {
            var newMovie = _mapper.Map<Movie>(movie);

            await _movieRepository.UploadImage(movie.ImageFile, newMovie);
            await _movieRepository.Add(newMovie);

            foreach (var newActorDto in movie.NewActors)
            {
                var newActor = _mapper.Map<Actor>(newActorDto);
                await _actorRepository.Add(newActor);
                await _actorRepository.UploadImage(newActorDto.ImageFile, newActor);

                await _movieActorRepository.Add(new MovieActor() { MovieId = newMovie.Id, ActorId = newActor.Id });
            }

            foreach (var actor in movie.ActorIds)
            {
                await _movieActorRepository.Add(new MovieActor() { MovieId = newMovie.Id, ActorId = actor });
            }

            foreach (var category in movie.CategoryIds)
            {
                await _movieCategoryRepository.Add(new MovieCategory() { MovieId = newMovie.Id, CategoryId = category });
            }
        }

        public async Task<MovieEditDto> EditGet(int id)
        {
            var result = _mapper.Map<MovieEditDto>(await _movieRepository.GetById(id));
            result.Actors = _actorRepository.GetAll().Select(a => new SelectListItem { Text = a.FullName, Value = a.Id.ToString() }).ToList();
            result.Categories = _categoryRepository.GetAll().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToList();
            return result;
        }

        public async Task EditPost(MovieEditDto movie)
        {
            var editedMovie = _mapper.Map<Movie>(movie);
            await _movieRepository.Update(editedMovie);
            await _movieRepository.UploadImage(movie.ImageFile, editedMovie);

            var movieActors = await _movieActorRepository.Search(ma => ma.MovieId == movie.Id);
            foreach (var ma in movieActors)
            {
                await _movieActorRepository.Remove(ma);
            }

            var movieCategories = await _movieCategoryRepository.Search(mc => mc.MovieId == movie.Id);
            foreach (var mc in movieCategories)
            {
                await _movieCategoryRepository.Remove(mc);
            }


            foreach (var actorId in movie.ActorIds)
            {
                await _movieActorRepository.Add(new MovieActor() { ActorId = actorId, MovieId = movie.Id });
            }

            foreach (var categoryId in movie.CategoryIds)
            {
                await _movieCategoryRepository.Add(new MovieCategory() { CategoryId = categoryId, MovieId = movie.Id });
            }

            foreach (var newActorDto in movie.NewActors)
            {
                var newActor = _mapper.Map<Actor>(newActorDto);
                await _actorRepository.Add(newActor);
                await _actorRepository.UploadImage(newActorDto.ImageFile, newActor);

                await _movieActorRepository.Add(new MovieActor() { MovieId = editedMovie.Id, ActorId = newActor.Id });
            }
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
            Filter(moviesForIndex);
            Sort(moviesForIndex);
            moviesForIndex.PaginatedMovies = PaginatedList<MovieDto>.Create(moviesForIndex.Movies.AsQueryable(), pageNumber ?? 1, pageSize);

            return moviesForIndex;
        }

        //-------------------------------------------------------------------------------
        private class MovieDtoComparer : IEqualityComparer<MovieDto>
        {
            public bool Equals([AllowNull] MovieDto first, [AllowNull] MovieDto second)
            {
                if (first.Id == second.Id)
                    return true;

                return false;
            }
            public int GetHashCode([DisallowNull] MovieDto obj)
            {
                return obj.Id.GetHashCode();
            }
        }
        //-------------------------------------------------------------------------------

        private void Filter(MovieIndexDto moviesForIndex)
        {
            if (moviesForIndex.CategoriesIds.Length != 0)
            {
                foreach (var category in moviesForIndex.CategoriesIds)
                {
                    var moviesWithCategory = _movieCategoryRepository.GetMoviesByCategory(category).ProjectTo<MovieDto>(_mapper.ConfigurationProvider).AsEnumerable();
                    moviesForIndex.Movies = moviesForIndex.Movies.Intersect(moviesWithCategory, new MovieDtoComparer());
                }
            }

            var filter = moviesForIndex.Movies.Where(m => m.ReleaseDate.Year >= moviesForIndex.YearMin && m.ReleaseDate.Year <= moviesForIndex.YearMax);
            moviesForIndex.Movies = filter.Where(m => m.Reviews.Any())
                                            .Where(m => m.Reviews
                                                .Average(m => m.Grade) >= moviesForIndex.GradeMin && m.Reviews
                                                    .Average(m => m.Grade) < moviesForIndex.GradeMax + 1);
                                            

            if(moviesForIndex.GradeMin == 0)
            {
                moviesForIndex.Movies = moviesForIndex.Movies.Union(filter.Where(m => m.Reviews.Any() == false));
            }

        }

        private void Sort(MovieIndexDto moviesForIndex)
        {
            switch (moviesForIndex.SortOrder)
            {
                case "nameDesc":
                    moviesForIndex.Movies = moviesForIndex.Movies.OrderByDescending(m => m.Name);
                    break;
                case "yearDesc":
                    moviesForIndex.Movies = moviesForIndex.Movies.OrderByDescending(m => m.ReleaseDate);
                    break;
                case "year":
                    moviesForIndex.Movies = moviesForIndex.Movies.OrderBy(m => m.ReleaseDate);
                    break;
                case "gradeDesc":
                    moviesForIndex.Movies = moviesForIndex.Movies.Where(m => m.Reviews.Any()).OrderByDescending(m => m.Reviews.Average(k => k.Grade)).Union(moviesForIndex.Movies);
                    break;
                case "grade":
                    moviesForIndex.Movies = moviesForIndex.Movies.Where(m => m.Reviews.Any()).OrderBy(m => m.Reviews.Average(k => k.Grade)).Union(moviesForIndex.Movies);
                    break;
                case "quantityGradeDesc":
                    moviesForIndex.Movies = moviesForIndex.Movies.Where(m => m.Reviews.Any()).OrderByDescending(m => m.Reviews.Count()).Union(moviesForIndex.Movies);
                    break;
                case "quantityGrade":
                    moviesForIndex.Movies = moviesForIndex.Movies.Where(m => m.Reviews.Any()).OrderBy(m => m.Reviews.Count()).Union(moviesForIndex.Movies);
                    break;
                default:
                    moviesForIndex.Movies = moviesForIndex.Movies.OrderBy(m => m.Name);
                    break;
            }
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

        public async Task DeleteImage(string imageName)
        {
            await _movieRepository.DeleteImage(imageName);
        }
    }
}
