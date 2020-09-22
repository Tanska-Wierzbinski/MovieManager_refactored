using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Application.DTOs.Movie
{
    public class MovieIndexDto
    {
        public IList<CategoryDto> Categories { get; set; }
    }
}
