using MovieManager.Application.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Application.DTOs.Movie
{
    public class MovieIndexDto : MovieDto
    {
        public IList<CategoryDto> Categories { get; set; }
    }
}
