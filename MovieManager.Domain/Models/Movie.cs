using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieManager.Domain.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }

        [MaxLength(4000)]
        public string ImageName { get; set; }
        public IList<Review> Reviews { get; set; }
        public IList<MovieCategory> MovieCategories { get; set; }
        public IList<MovieActor> MovieActors { get; set; }
    }
}
