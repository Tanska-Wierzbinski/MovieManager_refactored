using MovieManager.Application.DTOs.Home;
using MovieManager.Application.DTOs.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Application.Interfaces
{
    public interface IMovieService
    {
        MovieIndexDto GetAllForIndex(int yearMin, int yearMax, int gradeMin, int gradeMax, int[] categories, string sortOrder, int? pageNumber, int pageSize = 5);
        IndexDto GetForHome();
        SearchDto GetForSearch(string searchString);
        Task<MovieDetailsDto> GetDetails(int id);
        MovieAddDto AddGet();
        Task AddPost(MovieAddDto movie);
        Task<MovieEditDto> EditGet(int id);
        Task EditPost(MovieEditDto movie);
        Task<MovieDto> GetById(int id);
        Task<bool> Remove(int id);
        Task DeleteImage(string imageName);
    }
}
