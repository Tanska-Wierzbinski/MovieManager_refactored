using MovieManager.Application.DTOs.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieManager.Application.DTOs.Home
{
    public class IndexDto
    {
        public IQueryable<MovieDto> TopMovies { get; set; }
        public IQueryable<MovieDto> NewMovies { get; set; }
    }
}
