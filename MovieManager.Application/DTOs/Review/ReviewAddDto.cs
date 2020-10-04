using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieManager.Application.DTOs.Review
{
    public class ReviewAddDto
    {
        public string Description { get; set; }
        [Range(1, 10)]
        public int Grade { get; set; }
        public string Author { get; set; }
        public int MovieId { get; set; }
    }
}
