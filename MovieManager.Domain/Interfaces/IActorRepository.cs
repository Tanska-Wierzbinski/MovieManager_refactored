using Microsoft.AspNetCore.Http;
using MovieManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Domain.Interfaces
{
    public interface IActorRepository : IRepository<Actor>
    {
        Task UploadImage(IFormFile imageFile, Actor actor);
        Task DeleteImage(string imageName);
    }
}
