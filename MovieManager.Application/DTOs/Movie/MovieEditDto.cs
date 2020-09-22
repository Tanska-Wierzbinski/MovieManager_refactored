using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Application.DTOs.Movie
{
    public class MovieEditDto : MovieAddDto
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
    }
}
