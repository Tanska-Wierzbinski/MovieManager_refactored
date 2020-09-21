using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Domain.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int GradeValue { get; set; }
        public string Author { get; set; }
        public int MovieId { get; set; }
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
    }
}
