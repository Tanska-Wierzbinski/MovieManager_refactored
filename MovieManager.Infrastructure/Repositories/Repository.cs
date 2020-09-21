using Microsoft.EntityFrameworkCore;
using MovieManager.Domain.Interfaces;
using MovieManager.Domain.Models;
using MovieManager.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly MovieManagerContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(MovieManagerContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public virtual async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return  DbSet.AsNoTracking();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task Remove(TEntity entity)
        {
            DbSet.Remove(entity);
            await SaveChanges();
        }
        public virtual async Task UploadImage(Image image, TEntity entity)
        {
            await SaveChanges();
        }

        public virtual async Task DeleteImage(string imageName)
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

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }

    }
}
