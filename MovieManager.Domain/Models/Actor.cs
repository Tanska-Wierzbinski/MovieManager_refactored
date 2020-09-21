using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Domain.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? DeathDate { get; set; }
        public DateTime BornDate { get; set; }
        public Gender Gender { get; set; }

        public string Country { get; set; }

        public IList<MovieActor> MovieActors { get; set; }

        public IList<Grade> Grades { get; set; }

        public string ImageName { get; set; }

    }
}
