using MovieManager.Application.DTOs.Movie;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Application.Interfaces
{
    public interface IMovieService
    {
        Task<MovieIndexDto> GetAllForIndex();
        Task<MovieDetailsDto> GetDetails(int id);
        Task<MovieAddDto> AddGet();
        Task AddPost(MovieAddDto actor);
        Task<MovieEditDto> EditGet(int id);
        Task EditPost(MovieEditDto actor);
        Task<MovieDto> GetById();
        Task<bool> Remove(int id);
    }
}
