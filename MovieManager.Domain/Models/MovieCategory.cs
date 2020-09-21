using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Domain.Models
{
    public class MovieCategory
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
