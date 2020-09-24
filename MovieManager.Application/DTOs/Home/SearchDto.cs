using MovieManager.Application.DTOs.Actor;
using MovieManager.Application.DTOs.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieManager.Application.DTOs.Home
{
    public class SearchDto
    {
        public IQueryable<MovieDto> Movies { get; set; }
        public IQueryable<ActorDto> Actors { get; set; }
    }
}
