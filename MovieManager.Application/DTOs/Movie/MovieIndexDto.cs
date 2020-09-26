using MovieManager.Application.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Application.DTOs.Movie
{
    public class MovieIndexDto : MovieDto
    {
        public int YearMin { get; set; }
        public int YearMax { get; set; }
        public int GradeMin { get; set; }
        public int GradeMax { get; set; }
        public int[] CategoriesIds { get; set; }
        public string SortOrder { get; set; }
        public int? PageNumber { get; set; }
        public int PageSize { get; set; }

        public IEnumerable<MovieDto> Movies { get; set; }
        public PaginatedList<MovieDto> PaginatedMovies { get; set; }
        //public IEnumerable<CategoryDto> Categories { get; set; }
    }
}
