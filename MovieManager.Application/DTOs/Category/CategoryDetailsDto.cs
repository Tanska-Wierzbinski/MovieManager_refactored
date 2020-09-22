using MovieManager.Application.DTOs.Movie;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Application.DTOs.Category
{
    public class CategoryDetailsDto : CategoryDto
    {
        public IList<MovieDto> Movies { get; set; }
    }
}
