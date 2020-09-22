using MovieManager.Application.DTOs.Review;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Application.DTOs.Movie
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ImageName { get; set; }

        public IList<ReviewDto> Reviews { get; set; }
    }
}
