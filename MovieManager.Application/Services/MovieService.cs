using MovieManager.Application.DTOs.Movie;
using MovieManager.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Application.Services
{
    public class MovieService : IMovieService
    {
        public Task<MovieAddDto> AddGet()
        {
            throw new NotImplementedException();
        }

        public Task AddPost(MovieAddDto actor)
        {
            throw new NotImplementedException();
        }

        public Task<MovieEditDto> EditGet(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditPost(MovieEditDto actor)
        {
            throw new NotImplementedException();
        }

        public Task<MovieIndexDto> GetAllForIndex()
        {
            throw new NotImplementedException();
        }

        public Task<MovieDto> GetById()
        {
            throw new NotImplementedException();
        }

        public Task<MovieDetailsDto> GetDetails(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
