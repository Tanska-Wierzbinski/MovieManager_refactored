using Microsoft.AspNetCore.Http;
using MovieManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Domain.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task UploadImage(IFormFile imageFile, Movie movie);
        Task DeleteImage(string imageName);
    }
}
