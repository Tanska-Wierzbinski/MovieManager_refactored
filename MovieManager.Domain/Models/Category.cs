using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<MovieCategory> MovieCategories { get; set; }
    }
}
