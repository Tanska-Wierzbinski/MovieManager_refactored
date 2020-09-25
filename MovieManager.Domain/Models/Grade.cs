using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieManager.Domain.Models
{
    public class Grade
    {
        public int Id { get; set; }
        [Range(1,10)]
        public int GradeValue { get; set; }
        public int MovieId { get; set; }
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
        public string Author { get; set; }
    }
}
