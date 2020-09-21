using MovieManager.Domain.Interfaces;
using MovieManager.Domain.Models;
using MovieManager.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Infrastructure.Repositories
{
    public class MovieActorRepository : Repository<MovieActor>, IMovieActorRepository
    {
        public MovieActorRepository(MovieManagerContext context) : base(context)
        {

        }
    }
}
