using MovieManager.Application.DTOs.Grade;
using MovieManager.Application.DTOs.Movie;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Application.DTOs.Actor
{
    public class ActorDetailsDto : ActorDto
    {
        public IList<MovieDto> Movies { get; set; }
        
    }
}
