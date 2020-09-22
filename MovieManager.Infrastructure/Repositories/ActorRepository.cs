using Microsoft.AspNetCore.Hosting;
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
    public class ActorRepository : Repository<Actor>, IActorRepository
    {
        public ActorRepository(MovieManagerContext context, IWebHostEnvironment hostEnvironment) : base(context, hostEnvironment)
        {

        }

        public override async Task<Actor> GetById(int id)
        {
            return await Db.Actors.AsNoTracking()
                                  .Include(m => m.MovieActors)
                                  .ThenInclude(m => m.Movie)
                                  .Include(m => m.Grades)
                                  .SingleOrDefaultAsync(a => a.Id == id);
        }
        public override IQueryable<Actor> GetAll()
        {
            return Db.Actors.AsNoTracking()
                            .OrderBy(a => a.LastName)
                            .Include(a => a.Grades);
        }

        public override async Task UploadImage(Image image, Actor actor)
        {
            if (image != null)
            {
                if (actor != null)
                {
                    string wwwRothPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
                    string extension = Path.GetExtension(image.ImageFile.FileName);

                    fileName = fileName + DateTime.Now.ToString("yyMMddss") + extension;
                    image.Name = fileName;

                    string path = Path.Combine(wwwRothPath + "/Image/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        image.ImageFile.CopyTo(fileStream);
                    }

                    if (await Db.Images.SingleOrDefaultAsync(i => i.Name == actor.ImageName) != null)
                    {
                        var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Image", actor.ImageName);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        var img = await Db.Images.SingleOrDefaultAsync(i => i.Name == actor.ImageName);
                        Db.Images.Remove(img);
                        await Db.SaveChangesAsync();
                    }
                    await Db.Images.AddAsync(image);
                    actor.ImageName = image.Name;
                    await Db.SaveChangesAsync();
                }
            }
        }

    }
}
