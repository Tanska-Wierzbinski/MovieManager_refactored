using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MovieManager.Domain.Interfaces;
using MovieManager.Domain.Models;
using MovieManager.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MovieManager.Infrastructure.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        public MovieRepository(MovieManagerContext context, IWebHostEnvironment hostEnvironment) : base(context)
        {
            _hostEnvironment = hostEnvironment;
        }

        public override async Task<Movie> GetById(int id)
        {
            return await Db.Movies.AsNoTracking()
                                  .Include(m => m.MovieCategories)
                                  .ThenInclude(m => m.Category)
                                  .Include(m => m.MovieActors)
                                  .ThenInclude(m => m.Actor)
                                  .ThenInclude(m => m.Grades)
                                  .Include(m => m.Reviews)
                                  .SingleOrDefaultAsync(c => c.Id == id);
        }

        public override IQueryable<Movie> GetAll()
        {
            return Db.Movies.AsNoTracking()
                            .Include(m => m.Reviews)
                            .Include(m => m.MovieCategories)
                            .ThenInclude(m => m.Category);
        }
        public async Task UploadImage(IFormFile imageFile, Movie movie)
        {
            if (imageFile != null)
            {
                if (movie != null)
                {
                    string wwwRothPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                    string extension = Path.GetExtension(imageFile.FileName);

                    fileName = fileName + DateTime.Now.ToString("yyMMddss") + extension;
                    Image image = new Image();
                    image.Name = fileName;

                    string path = Path.Combine(wwwRothPath + "/Image/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        image.ImageFile.CopyTo(fileStream);
                    }

                    if (await Db.Images.SingleOrDefaultAsync(i => i.Name == movie.ImageName) != null)
                    {
                        var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Image", movie.ImageName);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        var img = await Db.Images.SingleOrDefaultAsync(i => i.Name == movie.ImageName);
                        Db.Images.Remove(img);
                        await Db.SaveChangesAsync();
                    }
                    await Db.Images.AddAsync(image);
                    movie.ImageName = image.Name;
                    await Db.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteImage(string imageName)
        {
            var result = await Db.Images.SingleOrDefaultAsync(c => c.Name == imageName);

            if (result != null)
            {
                var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Image", result.Name);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                Db.Images.Remove(result);
                await Db.SaveChangesAsync();
            }
        }
    }
}
