using MovieManager.Application.DTOs.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Application.DTOs.Movie
{
    public class MovieDetailsDto : MovieIndexDto
    {
        public IList<ActorDto> Actors { get; set; }
    }
}
