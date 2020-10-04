using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MovieManager.Domain.Interfaces;
using MovieManager.Domain.Models;
using MovieManager.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(MovieManagerContext context) : base(context)
        {

        }
        public override async Task<Category> GetById(int id)
        {
            return await Db.Categories.AsNoTracking()
                                      .SingleOrDefaultAsync(c => c.Id == id);
        }
        public override IQueryable<Category> GetAll()
        {
            return  Db.Categories.AsNoTracking()
                                  .OrderBy(c => c.Name);
        }
    }
}
