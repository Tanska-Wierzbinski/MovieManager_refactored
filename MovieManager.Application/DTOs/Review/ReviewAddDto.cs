using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Application.DTOs.Review
{
    public class ReviewAddDto
    {
        public string Description { get; set; }
        public int Grade { get; set; }
        public string Author { get; set; }
        public int MovieId { get; set; }
    }
}
